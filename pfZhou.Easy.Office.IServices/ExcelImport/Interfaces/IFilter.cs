
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.IServices.ExcelImport.Interfaces
{
    /// <summary>
    /// 过滤器的基接口
    /// </summary>
    public interface IFilter
    {
        List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption);
    }
}
