using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System.Threading.Tasks;

namespace pfZhou.Easy.Office.IServices.ExcelImport.Interfaces
{
    public interface IExcelImportService
    {
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="importOption"></param>
        /// <returns></returns>
        Task<ExcelData> ValidateAsync<Template>(ImportOption importOption) where Template:class,new();
    }
}
