using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.IO;

namespace pfZhou.Easy.Office.IServices.ExcelImport.Interfaces
{
    /// <summary>
    /// Excel导入的驱动接口
    /// </summary>
    public interface IExcelImportProvider
    {
        /// <summary>
        /// 将Sheet转换为ExcelData
        /// </summary>
        /// <typeparam name="TExcelTemplate"></typeparam>
        /// <param name="fileUrl"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        ExcelData Convert<TExcelTemplate>(Stream stream, int sheetIndex = 0, int headerIndex = 0, int rowIndex = 1) where TExcelTemplate : class, new();
    }
}
