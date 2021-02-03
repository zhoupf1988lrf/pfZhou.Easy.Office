using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// Excel数据行
    /// </summary>
    public class ExcelDataRow
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 是否合法
        /// </summary>
        public bool IsValid { get; set; } = true;
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMsg { get; set; } = string.Empty;
        /// <summary>
        /// 列的数据集合
        /// </summary>
        public List<ExcelDataCol> excelDataColList { get; set; } = new List<ExcelDataCol>();
    }
}
