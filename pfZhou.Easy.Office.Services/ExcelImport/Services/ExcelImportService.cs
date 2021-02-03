using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using pfZhou.Easy.Office.Services.ExcelImport.Factory;
using pfZhou.Easy.Office.Services.ExcelImport.Filters;
using System.Threading.Tasks;

namespace pfZhou.Easy.Office.Services.ExcelImport.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly IExcelImportProvider _excelImportProvider;
        public ExcelImportService(IExcelImportProvider excelImportProvider)
        {
            this._excelImportProvider = excelImportProvider;
        }
        public async Task<ExcelData> ValidateAsync<Template>(ImportOption importOption)
            where Template : class, new()
        {
            ExcelData excelData = this.GetExcelData<Template>(importOption);

            AndFilter andFilter = new AndFilter { Filters = FiltersFactory.CreateFilters<Template>() };

            FilterContext filterContext = new FilterContext { TypeFilter = TypeFilterInfoFactory.CreateInstance<Template>() };

            excelData.ExcelDataRows = andFilter.Filter(excelData.ExcelDataRows, filterContext, importOption);

            return await Task.FromResult(excelData);
        }
        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <typeparam name="TExcelTemplate"></typeparam>
        /// <param name="importOption"></param>
        /// <returns></returns>
        private ExcelData GetExcelData<TExcelTemplate>(ImportOption importOption)
            where TExcelTemplate : class, new()
        {
            return importOption.ExcelImportProvider == null ?
                this._excelImportProvider.Convert<TExcelTemplate>(
                stream: importOption.Stream,
                sheetIndex: importOption.SheetIndex,
                headerIndex: importOption.DataHeaderIndex,
                rowIndex: importOption.DataRowsIndex
                ) :
                importOption.ExcelImportProvider.Convert<TExcelTemplate>(
                    stream: importOption.Stream,
                    sheetIndex: importOption.SheetIndex,
                    headerIndex: importOption.DataHeaderIndex,
                    rowIndex: importOption.DataRowsIndex
                );
        }
    }
}
