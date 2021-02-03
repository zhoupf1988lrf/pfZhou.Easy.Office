using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelImport.Factory
{
    /// <summary>
    /// 模板类过滤器信息工厂
    /// </summary>
    public static class TypeFilterInfoFactory
    {
        /// <summary>
        /// 创建 模板类过滤信息
        /// </summary>
        /// <typeparam name="TExcelTemplate">Excel配置模板类</typeparam>
        /// <returns>模板类过滤信息</returns>
        public static TypeFilterInfo CreateInstance<TExcelTemplate>()
            where TExcelTemplate:class,new()
        {
            TypeFilterInfo typeFilterInfo = new TypeFilterInfo();
            var propertys = typeof(TExcelTemplate).GetProperties().Where(p => p.IsDefined(typeof(ColNameAttribute))).ToList();
            propertys.ForEach(p =>
            {
                typeFilterInfo.PropertyFilterInfos.Add(new PropertyFilterInfo
                {
                    PropertyName = p?.Name,
                    Filters = p.GetCustomAttributes<BaseFilterAttribute>().ToList()
                });
            });
            return typeFilterInfo;
        }
    }
}
