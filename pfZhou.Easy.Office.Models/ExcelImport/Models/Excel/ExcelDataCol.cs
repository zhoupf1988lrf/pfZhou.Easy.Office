using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// Excel数据列
    /// </summary>
   public class ExcelDataCol : DataCol
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 单元格值
        /// </summary>
        public string CellString { get; set; }
        /// <summary>
        /// 列是否合法
        /// </summary>
        public bool IsValid { get; set; } = true;
       
    }
}
