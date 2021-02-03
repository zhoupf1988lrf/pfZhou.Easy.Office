using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelExport;
using System;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    /// <summary>
    /// 创建单个过滤器工厂
    /// </summary>
    public static class FilterFactory
    {
        public static IFilter CreateInstance(BaseDecorationAttribute baseDecoration)
        {
            IFilter filter = null;
            Type type = Assembly.GetAssembly(typeof(PfZhouEasyOfficeCollectionExtensions)).GetTypes()
                 .Where(t => typeof(IFilter).IsAssignableFrom(t) && t.IsSubclassOf(typeof(BaseFilter)))
                 .FirstOrDefault(t => t.GetCustomAttribute<BindAttribute>().BindType == baseDecoration.GetType());
            if (type != null)
                filter = (IFilter)Activator.CreateInstance(type);
            return filter;
        }
    }
}
