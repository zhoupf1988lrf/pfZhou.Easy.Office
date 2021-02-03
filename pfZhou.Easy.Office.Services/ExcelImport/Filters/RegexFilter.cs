using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Core.ExcelImportExport.Enums;
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 正则 过滤器
    /// </summary>
    [FilterBind(typeof(RegexAttribute))]
    public class RegexFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (var dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == ImportMode.ErrorAfterStop) continue;
                dr.excelDataColList.ForEach(edc =>
                {
                    var attr = edc.GetAttribute<RegexAttribute>(filterContext.TypeFilter);
                    if (attr != null)
                        edc.SetNotValid(dr, Regex.IsMatch(edc.CellString, attr.Regex), attr.ErrorMsg);
                });
            }
            return dataRows;
        }
    }
}
