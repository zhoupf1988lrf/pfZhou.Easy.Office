using pfZhou.Easy.Office.Models.ExcelExport;

namespace pfZhou.Easy.Office.IServices.ExcelExport
{
    /// <summary>
    /// ExcelExport导出的驱动
    /// </summary>
    public interface IExcelExportProvider
    {
        /// <summary>
        /// 设置标题装饰（样式）
        /// </summary>
        /// <param name="workBookBytes"></param>
        /// <param name="context"></param>
        /// <param name="exportOption"></param>
        /// <returns></returns>
        byte[] SetTitleDecoration(byte[] workBookBytes, DecorationContext context, ExportOption exportOption);

    }
}
