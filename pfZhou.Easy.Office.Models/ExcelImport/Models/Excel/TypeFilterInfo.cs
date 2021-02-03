using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// ExcelDTO的类型过滤
    /// </summary>
    public class TypeFilterInfo
    {
        /// <summary>
        /// 属性过滤信息 集合
        /// </summary>
        public List<PropertyFilterInfo> PropertyFilterInfos { get; set; } = new List<PropertyFilterInfo>();
    }
}
