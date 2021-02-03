using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;

namespace pfZhou.Easy.Office.Core.ExcelExport
{
    public static class ExcelExportExtend
    {
        /// <summary>
        /// 获取列头的跨行数
        /// </summary>
        /// <param name="header"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static int GetRowSpan(this string header, int rows)
        {
            if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header), "参数不能是空或null");
            int count = header.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (rows == count) count = 1;
            else if (count < rows) count = 1 + (rows - count);
            else throw new NotSupportedException("表头格式不正确!");
            return count;
        }
        /// <summary>
        /// 获取列头的跨列数
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static int GetColSpan(this string header)
        {
            if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header), "参数不能是空或null");
            return header.Split(',').Length;
        }
        /// <summary>
        /// 获取标题头行数
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static int GetHeaderRowCount(this string header)
        {
            if (string.IsNullOrWhiteSpace(header)) return default;
            int count = 0;
            string[] columnNames = header.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string name in columnNames)
            {
                int tempCount = name.Split(' ').Length;
                if (tempCount > count) count = tempCount;
            }
            return count;
        }
        /// <summary>
        /// 获取标题头列数
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static int GetHeaderColCount(this string header)
        {
            if (string.IsNullOrWhiteSpace(header)) return default;
            string[] columnNames = header.Split('#');
            int count = columnNames.Length;
            foreach (string name in columnNames)
            {
                int tempCount = name.Split(',').Length;
                if (tempCount > 1)
                    count += tempCount - 1;
            }
            return count;
        }
        /// <summary>
        /// 表头行
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static int GetTitleRow(this string title)
        {
            return string.IsNullOrWhiteSpace(title) ? 0 : 1;
        }
        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="newCell"></param>
        /// <param name="dataColumn"></param>
        /// <param name="drValue"></param>
        public static void SetCellString(this ICell newCell, DataColumn dataColumn, string drValue)
        {
            switch (dataColumn.DataType.ToString())
            {
                case "System.String":
                    newCell.SetCellValue(drValue);
                    break;
                case "System.DateTime":
                    DateTime.TryParse(drValue, out DateTime dtValue);
                    newCell.SetCellValue(dtValue.ToString("yyyy-MM-dd"));
                    break;
                case "System.Boolen":
                    bool.TryParse(drValue, out bool bValue);
                    newCell.SetCellValue(bValue);
                    break;
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int.TryParse(drValue, out int iValue);
                    newCell.SetCellValue(iValue);
                    break;
                case "System.Double":
                case "System.Decimal":
                    double.TryParse(drValue, out double dValue);
                    newCell.SetCellValue(dValue);
                    break;
                case "System.DBNull":
                    newCell.SetCellValue("");
                    break;
                default:
                    newCell.SetCellValue("");
                    break;
            }
        }
        /// <summary>
        /// 通过分号分割字符串
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string[] SplitTextBySemicolonChar(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue)) throw new ArgumentNullException(nameof(stringValue));
            return stringValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static byte[] ConvertWorkBookToBytes(this IWorkbook workbook)
        {
            byte[] bytes = null;
            if (workbook == null) return bytes;
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                workbook.Write(memoryStream);
                bytes = memoryStream.ToArray();
                workbook = null;
            }
            return bytes;
        }
        public static IWorkbook ConvertBytesToWorkBook(this byte[] workbookBytes)
        {
            IWorkbook workbook;
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(workbookBytes))
            {
                workbook = WorkbookFactory.Create(memoryStream);
            }
            return workbook;
        }
    }
}
