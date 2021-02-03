using NPOI.SS.UserModel;
using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelImport.Services
{
    public class ExcelImportProvider : IExcelImportProvider
    {
        /// <summary>
        /// 将Sheet转换为ExcelData
        /// </summary>
        /// <typeparam name="TExcelTemplate">Excel导入的业务类</typeparam>
        /// <param name="fileUrl">文件的绝对路径</param>
        /// <param name="sheetIndex">Sheet索引 默认0</param>
        /// <param name="headerIndex">数据Header索引 默认0</param>
        /// <param name="rowIndex">数据Row索引 默认1</param>
        /// <returns></returns>
        public ExcelData Convert<TExcelTemplate>(Stream stream, int sheetIndex = 0, int headerIndex = 0, int rowIndex = 1)
            where TExcelTemplate : class, new()
        {
            IWorkbook workbook = this.GetWorkBook(stream);
            ISheet sheet = this.GetSheet(workbook, sheetIndex);
            ExcelDataHeader dataHeader = this.GetExcelDataHeader(sheet, headerIndex);
            List<ExcelDataRow> excelDataRows = new List<ExcelDataRow>();
            for (int i = rowIndex; i < sheet.PhysicalNumberOfRows; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || !row.Cells.Any(c => !string.IsNullOrWhiteSpace(c.GetCellValue()))) continue;
                excelDataRows.Add(this.ConvertToExcelDataRow<TExcelTemplate>(row, dataHeader));
            }
            return new ExcelData
            {
                ExcelDataHeader = dataHeader,
                ExcelDataRows = excelDataRows
            };
        }
        private IWorkbook GetWorkBook(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            IWorkbook workbook = WorkbookFactory.Create(stream);
            return workbook;
        }
        private ISheet GetSheet(IWorkbook workbook, int sheetIndex)
        {
            return workbook.GetSheetAt(sheetIndex);
        }
        /// <summary>
        /// 获取ExcelDataHeader
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="headerIndex"></param>
        /// <returns></returns>
        private ExcelDataHeader GetExcelDataHeader(ISheet sheet, int headerIndex)
        {
            ExcelDataHeader excelDataHeader = new ExcelDataHeader();
            IRow row = sheet.GetRow(headerIndex);
            for (int i = 0; i < row.PhysicalNumberOfCells; i++)
            {

                excelDataHeader.DataCols.Add(
                    new DataCol
                    {
                        ColIndex = i,
                        ColName = row.GetCell(i).GetCellValue()
                    }
                 );
            }
            return excelDataHeader;
        }
        /// <summary>
        /// 将IRow转换为ExcelDataRow
        /// </summary>
        /// <typeparam name="TExcelTemplate"></typeparam>
        /// <param name="row"></param>
        /// <param name="excelDataHeader"></param>
        /// <returns></returns>
        private ExcelDataRow ConvertToExcelDataRow<TExcelTemplate>(IRow row, ExcelDataHeader excelDataHeader)
            where TExcelTemplate : class, new()
        {
            var propertys = typeof(TExcelTemplate).GetProperties();
            ExcelDataRow excelDataRow = new ExcelDataRow
            {
                RowIndex = row.RowNum
            };

            propertys.ToList().ForEach(p =>
            {
                if (p.IsDefined(typeof(ColNameAttribute), true))
                {
                    ColNameAttribute colNameAttr = p.GetCustomAttribute<ColNameAttribute>();
                    var dataCol = excelDataHeader.DataCols.FirstOrDefault(dc => dc.ColName == colNameAttr.ColName);
                    excelDataRow.excelDataColList.Add(new ExcelDataCol
                    {
                        ColIndex = dataCol.ColIndex,
                        ColName = dataCol.ColName,
                        RowIndex = row.RowNum,
                        CellString = row.GetCell(dataCol.ColIndex).GetCellValue(),
                        PropertyName = p?.Name
                    });
                }
                else
                {
                    excelDataRow.excelDataColList.Add(new ExcelDataCol
                    {
                        ColIndex = 0,
                        ColName = "",
                        RowIndex = row.RowNum,
                        CellString = "",
                        PropertyName = p?.Name
                    });
                }
            });
            return excelDataRow;
        }
    }
}
