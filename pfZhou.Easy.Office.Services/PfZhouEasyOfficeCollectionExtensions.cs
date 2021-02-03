using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using pfZhou.Easy.Office.IServices.ExcelExport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Services.ExcelExport;
using pfZhou.Easy.Office.Services.ExcelImport.Services;

namespace pfZhou.Easy.Office.Services
{
    public static class PfZhouEasyOfficeCollectionExtensions
    {
        public static IServiceCollection AddPfZhouOffice(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IExcelImportService, ExcelImportService>();
            serviceCollection.AddTransient<IExcelImportProvider, ExcelImportProvider>();
            serviceCollection.AddTransient<IExcelExportService, ExcelExportService>();
            serviceCollection.AddTransient<IExcelExportProvider, ExcelExportProvider>();
            return serviceCollection;
        }
    }
}
