using NPOI.SS.UserModel;
using pfZhou.Easy.Office.Core.ExcelExport;
using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelExport;
using pfZhou.Easy.Office.Models.ExcelExport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    public class ExcelExportProvider : IExcelExportProvider
    {
        public byte[] SetTitleDecoration(byte[] workBookBytes, DecorationContext context, ExportOption exportOption)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (exportOption == null) return workBookBytes;

            if (string.IsNullOrWhiteSpace(exportOption.Title)) return workBookBytes;

            List<BaseDecorationAttribute> basesList = new List<BaseDecorationAttribute>();
            basesList.AddRange(context.TypeDecoInfo.TypeDecoAttrs);
            basesList.AddRange(context.TypeDecoInfo.propertyDecoInfos.SelectMany(pd => pd.PropertyAttrs));

            IWorkbook workbook = workBookBytes.ConvertBytesToWorkBook();

            var titleAttr = (TitleAttribute)basesList.FirstOrDefault(bd => bd.GetType() == typeof(TitleAttribute));

            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.Alignment = titleAttr.HorizontalAlignment;
            cellStyle.VerticalAlignment = titleAttr.VerticalAlignment;
            cellStyle.FillForegroundColor = titleAttr.FillForegroundColor;
            cellStyle.FillPattern = titleAttr.FillPattern;
            cellStyle.BorderBottom = titleAttr.BorderBottom;
            cellStyle.BorderLeft = titleAttr.BorderLeft;
            cellStyle.BorderRight = titleAttr.BorderRight;


            IFont font = workbook.CreateFont();
            font.Color = (short)titleAttr.FontColor.GetHashCode();
            font.FontName = titleAttr.FontName;
            font.FontHeightInPoints = titleAttr.FontSize;
            if (titleAttr.IsBold)
                font.IsBold = true;
            cellStyle.SetFont(font);


            ISheet sheet = workbook.GetSheet(exportOption.SheetName);
            IRow row = sheet.GetRow(0);
            for (int i = 0; i < row.PhysicalNumberOfCells; i++)
            {
                row.GetCell(i).CellStyle = cellStyle;
            }
            return workbook.ConvertWorkBookToBytes();
        }
    }
}
