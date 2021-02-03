using pfZhou.Easy.Office.Core.ExcelImportExport.Enums;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using System.IO;

namespace pfZhou.Easy.Office.IServices.ExcelImport
{
    /// <summary>
    /// 导入选项
    /// </summary>
    public class ImportOption
    {
        /// <summary>
        /// 二进制流
        /// </summary>
        public Stream Stream { get; set; }
        /// <summary>
        /// sheet工作簿索引 默认0，第一个sheet
        /// </summary>
        public int SheetIndex { get; set; } = 0;
        /// <summary>
        /// 表头索引  默认0，第一行
        /// </summary>
        public int DataHeaderIndex { get; set; } = 0;
        /// <summary>
        /// 数据索引 默认1，第二行
        /// </summary>
        public int DataRowsIndex { get; set; } = 1;
        /// <summary>
        /// 导入模式
        /// </summary>
        public ImportMode ImportMode { get; set; } = ImportMode.ErrorAfterStop;
        /// <summary>
        /// 自定义导入的Provider
        /// </summary>
        public IExcelImportProvider ExcelImportProvider { get; set; }
    }
}
