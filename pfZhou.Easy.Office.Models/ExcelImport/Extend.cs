using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Core.Extends;
using pfZhou.Easy.Office.Entities;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace pfZhou.Easy.Office.Models.ExcelImport
{
    public static class Extend
    {
        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellValue(this ICell cell)
        {
            string cellValue = string.Empty;
            switch (cell.CellType)
            {
                case CellType.Unknown:
                    break;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        cellValue = DateTime.FromOADate(cell.NumericCellValue).ToString("yyyy-MM-dd");
                    else
                        cellValue = cell.NumericCellValue.ToString();
                    break;
                case CellType.String:
                    cellValue = cell.StringCellValue;
                    break;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    cellValue = cell.BooleanCellValue.ToString();
                    break;
                case CellType.Error:
                    cellValue = cell.ErrorCellValue.ToString();
                    break;
                default:
                    break;
            }
            return cellValue;
        }
        /// <summary>
        /// 获取该属性的指定的特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="excelDataCol"></param>
        /// <param name="typeFilterInfo"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this ExcelDataCol excelDataCol, TypeFilterInfo typeFilterInfo)
            where TAttribute : BaseFilterAttribute
        {
            PropertyFilterInfo propertyFilter = typeFilterInfo.PropertyFilterInfos.FirstOrDefault(pfi => pfi.PropertyName == excelDataCol.PropertyName);
            if (propertyFilter == null) return null;
            var basefilterAttr = propertyFilter.Filters.FirstOrDefault(f => f.GetType() == typeof(TAttribute));
            return (TAttribute)basefilterAttr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelDataCol"></param>
        /// <param name="excelDataRow"></param>
        /// <param name="isValid"></param>
        /// <param name="errorMsg"></param>
        public static void SetNotValid(this ExcelDataCol excelDataCol, ExcelDataRow excelDataRow, bool isValid, string errorMsg)
        {
            if (!isValid)
            {
                excelDataRow.IsValid = false;
                excelDataRow.ErrorMsg += excelDataCol.ColName + errorMsg + ";";
            }
        }
        /// <summary>
        /// 判断该字符串是否是日期类型
        /// </summary>
        /// <param name="dtString"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string dtString)
        {
            return DateTime.TryParse(dtString, out DateTime result);
        }
        /// <summary>
        /// 判断该字符串的长度是否在指定区间内
        /// </summary>
        /// <param name="cellstring"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static bool IsRange(this string cellstring, int max, int min)
        {
            if (string.IsNullOrWhiteSpace(cellstring)) return false;
            if (!int.TryParse(cellstring, out int iResult)) return false;
            if (iResult > max || iResult < min) return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBusiness"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public static string DBCheck<TBusiness>(this BaseFilterAttribute baseFilter, string propertyName, string propertyValue)
          where TBusiness : Entity
        {
            string selectDBValue = string.Empty;
            var context = QHBXHttpContext.Current.RequestServices.GetService(typeof(DbContext)) as DbContext;
            Expression<Func<TBusiness, bool>> expression = t => t.IsDelete == DeleteStatus.Normal;
            var secondExp = ExpressionExtend.CreateExpressionEqual<TBusiness>(propertyName, propertyValue);
            expression = expression.And(secondExp);
            var result = context.Set<TBusiness>().FirstOrDefault(expression);
            var tableAttr = typeof(TBusiness).GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.TableAttribute>();
            if (baseFilter.GetType() == typeof(ForeignCheckAttribute))
            {
                if (result != null) selectDBValue = result.Id;
                else
                {
                    baseFilter.ErrorMsg = string.Format(baseFilter.ErrorMsg, tableAttr.Name);
                }
            }
            else if (baseFilter.GetType() == typeof(DBFieldDumplicateAttribute))
            {
                if (result != null)
                {
                    selectDBValue = result.Id;
                    baseFilter.ErrorMsg = string.Format(baseFilter.ErrorMsg, tableAttr.Name);
                }
            }
            return selectDBValue;
        }
        /// <summary>
        /// excelDatas转换为业务类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excelDatas"></param>
        /// <returns></returns>
        public static List<T> ConvertExcelDataToEntity<T>(this List<ExcelDataRow> excelDatas)
            where T : class, new()
        {
            List<T> list = new List<T>();
            var propertys = typeof(T).GetProperties();
            foreach (ExcelDataRow excelDataRow in excelDatas)
            {
                T t = new T();
                foreach (PropertyInfo property in propertys)
                {
                    var exceldataCol = excelDataRow.excelDataColList.FirstOrDefault(edc => edc.PropertyName == property.Name);
                    if (exceldataCol == null) continue;
                    if (property.PropertyType != typeof(string))
                    {
                        Type proType = property.PropertyType;
                        MethodInfo convertMethod = typeof(Core.Extends.Extend).GetMethod(nameof(Core.Extends.Extend.Convert)).MakeGenericMethod(proType);
                        var v = convertMethod.Invoke(null, new object[] { exceldataCol.CellString });
                        property.SetValue(t, v);
                    }
                    else if (property.Name == nameof(ExcelImportBase.ErrorMsg))
                    {
                        property.SetValue(t, excelDataRow.ErrorMsg);
                    }
                    else
                    {
                        property.SetValue(t, exceldataCol.CellString);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
