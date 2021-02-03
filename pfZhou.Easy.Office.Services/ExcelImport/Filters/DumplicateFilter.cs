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
    /// 重复过滤器
    /// </summary>
    [FilterBind(typeof(DumplicateAttribute))]
    public class DumplicateFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            List<KeyValuePair<int, string>> kvpList = new List<KeyValuePair<int, string>>();
            foreach (var dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == ImportMode.ErrorAfterStop) continue;
                dr.excelDataColList.ForEach(edc =>
                {
                    KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(edc.ColIndex, edc.CellString);
                    var attr = edc.GetAttribute<DumplicateAttribute>(filterContext.TypeFilter);
                    if (attr != null)
                        edc.SetNotValid(dr, !kvpList.Contains(kvp), attr.ErrorMsg);
                    kvpList.Add(kvp);
                });
            }
            return dataRows;
        }
    }
}
