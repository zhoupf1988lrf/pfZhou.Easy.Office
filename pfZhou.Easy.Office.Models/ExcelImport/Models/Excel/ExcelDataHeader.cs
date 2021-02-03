using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// Excel表头
    /// </summary>
    public class ExcelDataHeader
    {
        public List<DataCol> DataCols { get; set; } = new List<DataCol>();
    }
}
