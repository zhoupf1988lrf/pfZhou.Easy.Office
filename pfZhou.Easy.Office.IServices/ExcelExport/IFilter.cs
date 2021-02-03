using pfZhou.Easy.Office.Models.ExcelExport;

namespace pfZhou.Easy.Office.IServices.ExcelExport
{
    /// <summary>
    /// 过滤器基接口
    /// </summary>
    public interface IFilter
    {
        byte[] Filter(byte[] workBookBytes, DecorationContext context, ExportOption exportOption, IExcelExportProvider exportProvider);
    }
}
