using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Enums
{
    /// <summary>
    /// 导入模式
    /// </summary>
    public enum ImportMode
    {
        /// <summary>
        /// 错误后 停止导入后面行
        /// </summary>
        ErrorAfterStop,
        /// <summary>
        /// 继续
        /// </summary>
        Continue
    }
}
