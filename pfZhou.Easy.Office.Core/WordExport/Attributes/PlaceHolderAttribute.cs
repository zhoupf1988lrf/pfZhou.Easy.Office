using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.WordExport.Attributes
{
    /// <summary>
    /// 普通占位符特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PlaceHolderAttribute : Attribute
    {
        public PlaceHolderAttribute(string placeholder)
        {
            this.PlaceHolder = placeholder;
        }
        public string PlaceHolder { get; set; }
    }
}
