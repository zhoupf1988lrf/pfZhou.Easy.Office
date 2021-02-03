using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Models.ExcelImport.Models.Excel
{
    /// <summary>
    /// 属性过滤信息
    /// </summary>
    public class PropertyFilterInfo
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 特性集合
        /// </summary>
        public List<BaseFilterAttribute> Filters { get; set; }
    }
}
