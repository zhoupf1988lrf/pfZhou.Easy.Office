using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace pfZhou.Easy.Office.IServices.ExcelExport
{
    public interface IExcelExportService
    {
        /// <summary>
        /// Excel数据导出
        /// </summary>
        /// <typeparam name="TExport"></typeparam>
        /// <param name="exportOption">导出项设置类</param>
        /// <param name="datas">数据</param>
        byte[] ExportExcel<TExport>(ExportOption exportOption, List<TExport> datas)
            where TExport : class, new();
    }
}
