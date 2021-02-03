using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace pfZhou.Easy.Office.Core.Extends
{
    /// <summary>
    /// 表达式的扩展类
    /// </summary>
    public static class ExpressionExtend
    {
        /// <summary>
        /// 第一种：s => new { Name = name, Address = address, Contacts = contacts }表达式的body=NewExpression（匿名对象不包括初始化属性成员）
        /// 第二种：s => new { s.name,s. address,s.contacts }表达式的body=NewExpression（匿名对象不包括初始化属性成员）
        /// 以上两种匿名方式是相同的，第一种是自定义了变量名，第二种是采用默认的变量名 即属性名
        /// 
        /// 
        /// s=>new StoreEntity{Name=name}表达式的body=MemberInitExpression
        /// </summary>
        /// <typeparam name="T">当前的实例</typeparam>
        /// <typeparam name="TProperty">实例的属性</typeparam>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public static List<string> GetPropertys<T, TProperty>(this Expression<Func<T, TProperty>> propertys)
            where T : class
        {
            //NewExpression Represents a constructor call.
            //MemberExpression Represents accessing a field or property.
            //MemberInitExpression  Represents calling a constructor and initializing one or more members of the new object.

            List<string> propList = new List<string>();
            var memberExp = propertys.Body as MemberExpression;
            if (memberExp != null)
            {
                propList.Add(memberExp.Member.Name);
                return propList;
            }
            var newExp = propertys.Body as NewExpression;
            if (newExp != null)
            {
                foreach (var member in newExp.Members)
                {
                    propList.Add(member.Name);
                }
                return propList;
            }
            var memberInitExp = propertys.Body as MemberInitExpression;
            if (memberInitExp != null)
            {
                foreach (MemberBinding memberBinding in memberInitExp.Bindings)
                {
                    propList.Add(memberBinding.Member.Name);
                }

            }
            return propList;
        }
        /// <summary>
        /// 返回 predicate=>predicate.propertyName==value
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> CreateExpressionEqual<TSource>(string propertyName, string value)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "predicate");
            MemberExpression member = Expression.Property(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(value, typeof(string));
            var body = Expression.Equal(member, constant);
            return Expression.Lambda<Func<TSource, bool>>(body, new ParameterExpression[] { parameter });
        }

        /// <summary>
        /// 返回 predicate=>predicate.propertyName
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, TResult>> CreateExpression<TSource, TResult>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "predicate");
            MemberExpression member = Expression.Property(parameter, propertyName);
            return Expression.Lambda<Func<TSource, TResult>>(member, new ParameterExpression[] { parameter });
        }
        /// <summary>
        /// t=>new TSource{Status=t.Status,Updator=t.Updator,A=t.A,B=t.B}   表达式的body=MemberInitExpression
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="structureProptyNames"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, TSource>> CreateCustomPorpertyStructure<TSource>(this List<string> structureProptyNames)
            where TSource : class
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "t");
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var propName in structureProptyNames)
            {
                PropertyInfo property = typeof(TSource).GetProperty(propName);
                MemberExpression member = Expression.Property(parameter, propName);
                memberBindings.Add(Expression.Bind(property, member));
            }
            MemberInitExpression memberInit = Expression.MemberInit(Expression.New(typeof(TSource)), memberBindings);
            Expression<Func<TSource, TSource>> expression = Expression.Lambda<Func<TSource, TSource>>(memberInit, parameter);
            return expression;
        }



        /// <summary>
        /// Func<People,MyAnon> func = p => new TempQhbx{ Id = p.Id, Name = p.Name, Age = p.Age };
        /// MyAnon 是在运行时动态生成的类
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="propertyNames"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static (Func<TIn, TOut>, Type) CreateDynamicExpress<TIn, TOut>(this List<string> propertyNames)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "predicate");
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            Type anonymousType = CreateNewType<TIn>(propertyNames);
            var obj = Activator.CreateInstance(anonymousType);
            foreach (string propertyName in propertyNames)
            {
                PropertyInfo propertyInfo = typeof(TIn).GetProperty(propertyName);
                MemberExpression member = Expression.Property(parameterExpression, propertyInfo);
                memberBindings.Add(Expression.Bind(obj.GetType().GetField(propertyName), member));
            }
            var memberInit = Expression.MemberInit(Expression.New(anonymousType), memberBindings);
            var expression = Expression.Lambda<Func<TIn, TOut>>(memberInit, parameterExpression);
            return (expression.Compile(), anonymousType);
        }
        public static Type CreateNewType<TIn>(List<string> propertyNames)
        {
            AssemblyName dynamicAssemblyName = new AssemblyName("MyTempQhbx");
            AssemblyBuilder dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("MyTempQhbx");
            TypeBuilder dynamicAnonymousType = dynamicModule.DefineType("TempQhbx", TypeAttributes.Public);
            foreach (string propertyName in propertyNames)
            {
                FieldInfo dynamicfield = dynamicAnonymousType.DefineField(propertyName, typeof(TIn).GetProperty(propertyName).PropertyType, FieldAttributes.Public);
            }
            return dynamicAnonymousType.CreateTypeInfo();
        }
    }

    /// <summary>
    /// 统一ParameterExpression
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }

    public static class PredicateExtensionses
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            if (exp_left == null)
                return exp_right;
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            if (exp_left == null)
                return exp_right;
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }





    public static class Extend
    {
        public static System.Data.DataTable ConvertListToDataTable<TSource>(this IEnumerable<object> list)
            where TSource : class, new()
        {
            Type type = typeof(TSource);
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var t in list)
            {
                DataRow dr = dataTable.NewRow();
                foreach (var field in fields)
                {
                    if (!dr.Table.Columns.Contains(field.Name))
                        dr.Table.Columns.Add(field.Name, field.FieldType.Name == typeof(Nullable<>).Name ? field.FieldType.GetGenericArguments()[0] : field.FieldType);
                    //dr.Table.Columns.Add(field.Name, field.FieldType);//System.NotSupportedException:“DataSet does not support System.Nullable<>.”

                    dr[field.Name] = field.GetValue(t);
                }
                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }
        public static T Convert<T>(this object v)
        {
            if (v != null)
            {
                if (typeof(T) == v.GetType())
                    return (T)v;
                else if (typeof(T) == typeof(int))
                {
                    int num = 0;
                    int.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(float))
                {
                    float num = 0;
                    float.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(double))
                {
                    double num = 0;
                    double.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(decimal))
                {
                    decimal num = 0;
                    decimal.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(byte))
                {
                    byte num = 0;
                    byte.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(char))
                {
                    char num = ' ';
                    char.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(decimal))
                {
                    decimal num = ' ';
                    decimal.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(string))
                {
                    return (T)(object)(v + string.Empty);
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    DateTime num = DateTime.Now;
                    DateTime.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else if (typeof(T) == typeof(Boolean))
                {
                    bool num = false;
                    bool.TryParse(v + string.Empty, out num);
                    return (T)(object)num;
                }
                else
                    return (T)v;
            }
            if (typeof(T) == typeof(string))
                return (T)(object)string.Empty;
            return default(T);
        }
    }
}
