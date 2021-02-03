using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Services.ExcelImport.Filters;
using System;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelImport.Factory
{
    /// <summary>
    ///模板类 过滤器工厂
    /// </summary>
    public static class FilterFactory
    {
        public static IFilter CreateInstance(BaseFilterAttribute baseFilterAttribute)
        {
            var filter = Assembly.GetAssembly(typeof(PfZhouEasyOfficeCollectionExtensions)).GetTypes()
                 .Where(t => typeof(IFilter).IsAssignableFrom(t) && t.IsSubclassOf(typeof(BaseFilter)))
                 .FirstOrDefault(t => t.GetCustomAttribute<FilterBindAttribute>().BindType == baseFilterAttribute.GetType());
            if (filter == null) return default;
            return (IFilter)Activator.CreateInstance(filter);
        }
    }
}
