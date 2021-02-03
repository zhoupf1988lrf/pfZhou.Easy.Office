using System.Collections.Generic;
using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Core.ExcelImportExport.Enums;
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 日期 过滤器
    /// </summary>
    [FilterBind(typeof(DateTimeAttribute))]
    public class DateTimeFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (var dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == ImportMode.ErrorAfterStop) continue;
                dr.excelDataColList.ForEach(edc =>
                {
                    var attr = edc.GetAttribute<DateTimeAttribute>(filterContext.TypeFilter);
                    if (attr != null)
                        edc.SetNotValid(dr, edc.CellString.IsDateTime(), attr.ErrorMsg);
                });
            }
            return dataRows;
        }
    }
}
