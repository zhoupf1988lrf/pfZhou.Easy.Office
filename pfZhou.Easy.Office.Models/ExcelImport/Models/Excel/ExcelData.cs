using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// Excel数据
    /// </summary>
    public class ExcelData
    {
        public ExcelDataHeader ExcelDataHeader { get; set; }
        public List<ExcelDataRow> ExcelDataRows { get; set; } = new List<ExcelDataRow>();

    }
}
