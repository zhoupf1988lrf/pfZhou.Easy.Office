using System;
using System.Collections.Generic;
using System.Text;
using pfZhou.Easy.Office.Models.ExcelExport;

namespace pfZhou.Easy.Office.Models.ExcelExport
{
    /// <summary>
    /// 装饰器的上下文
    /// </summary>
    public class DecorationContext
    {
        /// <summary>
        /// DTO类型的装饰信息
        /// </summary>
        public TypeDecoInfo TypeDecoInfo { get; set; }
    }
}
