using pfZhou.Easy.Office.Core.ExcelImportExport.Utils;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Collections.Generic;
using System.Linq;

namespace pfZhou.Easy.Office.Services.ExcelImport.Factory
{
    /// <summary>
    /// 模板类过滤器集合享元工厂
    /// </summary>
    public static class FiltersFactory
    {
        /// <summary>
        /// 创建模板类的过滤器集合
        /// </summary>
        /// <returns></returns>
        public static List<IFilter> CreateFilters<TExcelTemplate>() where TExcelTemplate : class, new()
        {
            List<IFilter> filterList = new List<IFilter>();
            TypeFilterInfo typeFilterInfo = TypeFilterInfoFactory.CreateInstance<TExcelTemplate>();
            var attrs = typeFilterInfo.PropertyFilterInfos.SelectMany(pfi => pfi.Filters).ToList();
            attrs.Distinct(new BaseFilterAttrEqualCompare()).ToList().ForEach(bfa =>
            {
                var filter = FilterFactory.CreateInstance(bfa);
                if (filter != null) filterList.Add(filter);
            });
            return filterList;
        }
    }
}
