using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 特性的基类
    /// </summary>
    public class BaseFilterAttribute : Attribute
    {
        public string ErrorMsg { get; set; } = "非法";
    }
}
