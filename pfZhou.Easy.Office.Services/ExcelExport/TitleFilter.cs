using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelExport;
using pfZhou.Easy.Office.Models.ExcelExport;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    /// <summary>
    /// 标题过滤器
    /// </summary>
    [Bind(typeof(TitleAttribute))]
    public class TitleFilter : BaseFilter, IFilter
    {
        public byte[] Filter(byte[] workBookBytes, DecorationContext context, ExportOption exportOption, IExcelExportProvider exportProvider)
        {
            return exportProvider.SetTitleDecoration(workBookBytes, context, exportOption);
        }
    }
}
