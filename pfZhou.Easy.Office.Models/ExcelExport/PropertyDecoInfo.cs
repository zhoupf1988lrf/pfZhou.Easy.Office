using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Models.ExcelExport
{
    /// <summary>
    /// 属性装饰信息
    /// </summary>
    public class PropertyDecoInfo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 属性的特性集合
        /// </summary>
        public List<BaseDecorationAttribute> PropertyAttrs { get; set; } = new List<BaseDecorationAttribute>();
    }
}
