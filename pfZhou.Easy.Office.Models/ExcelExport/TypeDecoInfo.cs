using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Models.ExcelExport
{
    /// <summary>
    /// DTO类的 装饰信息
    /// </summary>
    public class TypeDecoInfo
    {
        /// <summary>
        /// 类的 特性集合
        /// </summary>
        public List<BaseDecorationAttribute> TypeDecoAttrs { get; set; } = new List<BaseDecorationAttribute>();
        /// <summary>
        /// 属性装饰信息的集合
        /// </summary>
        public List<PropertyDecoInfo> propertyDecoInfos { get; set; } = new List<PropertyDecoInfo>();
    }
}
