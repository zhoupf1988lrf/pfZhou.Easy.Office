
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 且 过滤器
    /// </summary>
    public class AndFilter : IFilter
    {
        public List<IFilter> Filters { get; set; }

        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (var filter in Filters)
            {
                filter.Filter(dataRows, filterContext, importOption);
            }
            return dataRows;
        }
    }
}
