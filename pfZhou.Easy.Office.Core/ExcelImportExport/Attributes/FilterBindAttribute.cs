using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 过滤器绑定
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FilterBindAttribute : Attribute
    {
        public Type BindType { get; set; }
        public FilterBindAttribute(Type type)
        {
            this.BindType = type;
        }
    }
}
