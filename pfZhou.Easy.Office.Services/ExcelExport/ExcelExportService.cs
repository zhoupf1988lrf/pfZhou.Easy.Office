using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using pfZhou.Easy.Office.Core.ExcelExport;
using pfZhou.Easy.Office.Core.Extends;
using pfZhou.Easy.Office.IServices.ExcelExport;
using pfZhou.Easy.Office.Models.ExcelExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extend = pfZhou.Easy.Office.Core.Extends;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    public class ExcelExportService : IExcelExportService
    {
        readonly IExcelExportProvider _exportProvider;
        public ExcelExportService(IExcelExportProvider excelExportProvider)
        {
            this._exportProvider = excelExportProvider;
        }

        public byte[] ExportExcel<TExport>(ExportOption exportOption, List<TExport> datas) where TExport : class, new()
        {
            var provider = this._exportProvider;
            if (exportOption.ExcelExportProvider != null)
                provider = exportOption.ExcelExportProvider;

            IWorkbook workbook = null;
            if (exportOption.ExcelType == Core.ExcelExport.Enums.ExcelType.XLS)
            {
                workbook = new HSSFWorkbook();
                if (datas.Count > 65536)
                    throw new NotSupportedException("xls文档最大长度是65536行");
            }
            else if (exportOption.ExcelType == Core.ExcelExport.Enums.ExcelType.XLSX)
            {
                workbook = new XSSFWorkbook();
                if (datas.Count > 1048576)
                    throw new NotSupportedException("xlsx文档最大长度是1048576行");
            }
            if (workbook == null) throw new NotSupportedException($"{nameof(workbook)}不能是null");

            DataSet dataSet = this.FindDataSource<TExport>(datas, exportOption.PropertyNames);

            byte[] workbookBytes = this.Export(exportOption, workbook, dataSet);

            DecorationContext context = new DecorationContext { TypeDecoInfo = TypeDecoInfoFactory.CreateTypeDecoInfo<TExport>() };

            List<IFilter> filters = FiltersFactory.CreateFilters(context);

            filters.ForEach(f =>
            {
                workbookBytes = f.Filter(workbookBytes, context, exportOption, provider);
            });

            return workbookBytes;
        }
        /// <summary>
        /// 将IEnumerable^1[Qhbx.Tibet.Wms.Models.ExcelExport.ExcelExportTemplate.TExport] 按照指定属性集合转换为DataSet
        /// </summary>
        /// <typeparam name="TExport"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        private DataSet FindDataSource<TExport>(IEnumerable<TExport> list, List<string> propertys) where TExport : class, new()
        {
            var result = propertys.CreateDynamicExpress<TExport, object>();
            var data = list.Select(result.Item1).ToList();
            var method = typeof(Extend.Extend).GetMethod(nameof(Extend.Extend.ConvertListToDataTable)).MakeGenericMethod(result.Item2);
            DataTable dt = (DataTable)method.Invoke(null, new object[] { data });
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }





        /// <summary>
        /// 导出Excel
        /// </summary>
        private byte[] Export(ExportOption exportOption, IWorkbook workbook, DataSet dataSet)
        {
            string[] tableHeaders = exportOption.Header.SplitTextBySemicolonChar();
            string[] sheetNames = exportOption.SheetName.SplitTextBySemicolonChar();
            string[] titles = exportOption.Title.SplitTextBySemicolonChar();

            ICellStyle titleStyle = this.titleStyleFunc(workbook);
            ICellStyle headerStyle = this.headerStyleFunc(workbook);
            ICellStyle contentStyle = this.styleFunc(workbook);
            for (int sheetIndex = 0; sheetIndex < sheetNames.Length; sheetIndex++)
            {
                ISheet sheet = workbook.CreateSheet(sheetNames[sheetIndex]);
               
                var rowInfo = this.CreateTitleAndHeader(tableHeaders, titles, sheetIndex, sheet, titleStyle, headerStyle);


                int rowIndex = rowInfo.headerRowCount + rowInfo.titleRowCount;
                this.CreateDataContent(sheetIndex, rowIndex, sheet, contentStyle, dataSet);
            }
            return workbook.ConvertWorkBookToBytes();
        }


        /// <summary>
        /// 创建标题和复杂表头
        /// </summary>
        /// <param name="tableHeaders"></param>
        /// <param name="titles"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="sheet"></param>
        /// <param name="titleStyle"></param>
        /// <param name="headerStyle"></param>
        /// <returns></returns>
        private (int headerRowCount, int titleRowCount) CreateTitleAndHeader(string[] tableHeaders, string[] titles, int sheetIndex, ISheet sheet, ICellStyle titleStyle, ICellStyle headerStyle)
        {
            string[] newHeaders = tableHeaders[sheetIndex].Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            int headerRowCount = tableHeaders[sheetIndex].GetHeaderRowCount();
            int headerColCount = tableHeaders[sheetIndex].GetHeaderColCount();
            int titleRow = titles[sheetIndex].GetTitleRow();
            string titleName = titles[sheetIndex];
            //sheet.DefaultColumnWidth = 8 * 256; //在sheet设置宽度时，Excel打开时，会是 受保护视图 状态。

            int cols = 0;//列计数器
            for (int m = 0; m < headerRowCount + titleRow; m++)//创建行
            {
                IRow row = sheet.CreateRow(m);
                if (m == 0 && titleRow > 0)
                {
                    row.Height = 50 * 20;
                    CellRangeAddress region = new CellRangeAddress(0, 0, 0, headerColCount - 1);
                    sheet.AddMergedRegion(region);
                    row.CreateCell(0).SetCellValue(titleName);
                    row.GetCell(0).CellStyle = titleStyle;
                    if (sheet is HSSFSheet)
                        ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.Thin, HSSFColor.Black.Index);
                    continue;
                }
                cols = 0;
                for (int i = 0; i < newHeaders.Length; i++)//创建列    
                {
                    sheet.SetColumnWidth(i,18*256);
                    string tempHeader = newHeaders[i];
                    int temprowspan = tempHeader.GetRowSpan(headerRowCount);
                    int tempcolspan = tempHeader.GetColSpan();
                    string[] temp = tempHeader.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp.Length == headerRowCount) tempHeader = temp[m - titleRow];
                    else tempHeader = temp[0];
                    if (temprowspan == 1)
                    {
                        row = sheet.GetRow(m);
                        if (row == null) row = sheet.CreateRow(m);
                        if (tempcolspan == 1)//未跨列
                        {
                            row.CreateCell(cols).SetCellValue(tempHeader);
                            row.GetCell(cols).CellStyle = headerStyle;
                        }
                        else
                        {
                            temp = tempHeader.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (temp.Length > 1)
                            {
                                for (int j = 0; j < temp.Length; j++)
                                {
                                    row.CreateCell(j + cols).SetCellValue(temp[j]);
                                    row.GetCell(j + cols).CellStyle = headerStyle;
                                }
                            }
                            else
                            {
                                //创建范围
                                CellRangeAddress cellRangeAddress = new CellRangeAddress(m, m, cols, cols + tempcolspan - 1);
                                sheet.AddMergedRegion(cellRangeAddress);
                                row.CreateCell(cols).SetCellValue(tempHeader);
                                row.GetCell(cols).CellStyle = headerStyle;
                                if (sheet is HSSFSheet)
                                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellRangeAddress, BorderStyle.Thin, HSSFColor.Black.Index);
                            }
                            cols += tempcolspan - 1;
                        }
                    }
                    else if (temprowspan > 1 && m < 4)
                    {
                        row = sheet.GetRow(m);
                        if (row == null) row = sheet.CreateRow(m);
                        if (tempcolspan == 1)//未跨列
                        {
                            CellRangeAddress cellRangeAddress = new CellRangeAddress(m, temprowspan - 1 + titleRow, cols, cols);
                            sheet.AddMergedRegion(cellRangeAddress);//NPOI最新版的sdk（2.50以上），必须要求 合并的单元格区域必须要有2个行或2个列。否报报错【Merged region A3 must contain 2 or more cells】。
                            row.CreateCell(cols).SetCellValue(tempHeader);
                            row.GetCell(cols).CellStyle = headerStyle;
                            if (sheet is HSSFSheet)
                                ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellRangeAddress, BorderStyle.Thin, HSSFColor.Black.Index);
                        }
                        else
                        {
                            CellRangeAddress region = new CellRangeAddress(m, temprowspan - 1 + titleRow, cols, cols + tempcolspan - 1);
                            sheet.AddMergedRegion(region);
                            row.CreateCell(cols).SetCellValue(tempHeader);
                            row.GetCell(cols).CellStyle = headerStyle;
                            cols += tempcolspan - 1;
                            if (sheet is HSSFSheet)
                                ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.Thin, HSSFColor.Black.Index);
                        }
                    }
                    cols += 1;//列的计数器
                }
            }
            return (headerRowCount, titleRow);
        }
        /// <summary>
        /// 创建内容
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="sheet"></param>
        /// <param name="contentStyle"></param>
        /// <param name="dsSource"></param>
        private void CreateDataContent(int sheetIndex, int rowIndex, ISheet sheet, ICellStyle contentStyle, DataSet dsSource)
        {
            foreach (DataRow dr in dsSource.Tables[sheetIndex].Rows)
            {
                var drrow = sheet.CreateRow(rowIndex);
                foreach (DataColumn dataColumn in dsSource.Tables[sheetIndex].Columns)
                {
                    var newCell = drrow.CreateCell(dataColumn.Ordinal);
                    string drValue = dr[dataColumn].ToString();
                    newCell.SetCellString(dataColumn, drValue);
                    newCell.CellStyle = contentStyle;
                }
                rowIndex++;
            }
        }

        /// <summary>
        /// 表格内容单元格样式
        /// </summary>
        Func<IWorkbook, ICellStyle> styleFunc = workbook =>
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;//换行
                                  //边框
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            //字体
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "宋体";
            style.SetFont(font);
            return style;
        };
        /// <summary>
        /// 表格头部单元格样式
        /// </summary>
        Func<IWorkbook, ICellStyle> headerStyleFunc = workbook =>
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.FillForegroundColor = HSSFColor.Yellow.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "宋体";
            style.SetFont(font);
            return style;
        };
        /// <summary>
        /// 标题单元格样式
        /// </summary>
        Func<IWorkbook, ICellStyle> titleStyleFunc = workbook =>
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.FillForegroundColor = HSSFColor.LightGreen.Index;
            style.FillPattern = FillPattern.SolidForeground;

            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 14;
            font.FontName = "宋体";
            font.IsBold = true;
            style.SetFont(font);
            return style;
        };
    }
}
