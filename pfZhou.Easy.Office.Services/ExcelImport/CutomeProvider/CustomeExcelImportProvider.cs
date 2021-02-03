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

namespace pfZhou.Easy.Office.Services.ExcelImport.CutomeProvider
{
    /// <summary>
    /// 自定义Excel导入的provider
    /// </summary>
    public class CustomeExcelImportProvider : IExcelImportProvider
    {
        public ExcelData Convert<TExcelTemplate>(Stream stream, int sheetIndex = 0, int headerIndex = 0, int rowIndex = 1) where TExcelTemplate : class, new()
        {
            ExcelDataHeader excelDataHeader = new ExcelDataHeader();
            List<ExcelDataRow> excelDataRows = new List<ExcelDataRow>();
            IWorkbook workbook = WorkbookFactory.Create(stream);
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            excelDataHeader = this.GetHeader(sheet, headerIndex);
            for (int i = rowIndex; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null || !row.Cells.Any(c => !string.IsNullOrWhiteSpace(c.GetCellValue()))) continue;
                excelDataRows.Add(this.ConvertToExcelDataRow<TExcelTemplate>(row, excelDataHeader));
            }
            return new ExcelData
            {
                ExcelDataHeader = excelDataHeader,
                ExcelDataRows = excelDataRows
            };
        }
        protected ExcelDataHeader GetHeader(ISheet sheet, int headerIndex)
        {
            ExcelDataHeader excelDataHeader = new ExcelDataHeader();
            IRow row = sheet.GetRow(headerIndex);
            for (int col = 0; col < row.PhysicalNumberOfCells; col++)
            {
                excelDataHeader.DataCols.Add(new DataCol
                {
                    ColIndex = col,
                    ColName = row.GetCell(col).GetCellValue()
                });
            }
            return excelDataHeader;
        }
        /// <summary>
        /// 将IRow转换为ExcelDataRow
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="row"></param>
        /// <param name="excelDataHeader"></param>
        /// <returns></returns>
        public ExcelDataRow ConvertToExcelDataRow<TSource>(IRow row, ExcelDataHeader excelDataHeader)
            where TSource : class, new()
        {
            ExcelDataRow excelDataRow = new ExcelDataRow
            {
                RowIndex = row.RowNum
            };
            var propertys = typeof(TSource).GetProperties();
            for (int i = 0; i < excelDataHeader.DataCols.Count; i++)
            {
                DataCol dataCol = excelDataHeader.DataCols.FirstOrDefault(dc => dc.ColIndex == i);
                PropertyInfo matchProperty = propertys.FirstOrDefault(p => p.GetCustomAttribute<ColNameAttribute>().ColName == dataCol?.ColName);
                excelDataRow.excelDataColList.Add(new ExcelDataCol
                {
                    ColIndex = i,
                    ColName = dataCol.ColName,
                    RowIndex = row.RowNum,
                    CellString = row.GetCell(i).GetCellValue(),
                    PropertyName = matchProperty?.Name
                });
            }
            return excelDataRow;
        }
    }
}
