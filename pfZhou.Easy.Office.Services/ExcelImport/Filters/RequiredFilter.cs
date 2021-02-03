using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Core.ExcelImportExport.Enums;
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 必填 过滤器
    /// </summary>
    [FilterBindAttribute(typeof(RequiredAttribute))]
    public class RequiredFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (var dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == ImportMode.ErrorAfterStop) continue;
                dr.excelDataColList.ForEach(edc =>
                {
                    var attr = edc.GetAttribute<RequiredAttribute>(filterContext.TypeFilter);
                    if (attr != null)
                    {
                        edc.SetNotValid(dr, !string.IsNullOrWhiteSpace(edc.CellString), attr.ErrorMsg);
                    }
                });
            }
            return dataRows;
        }
    }
}
