using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelExport.Attributes
{
    /// <summary>
    /// 此类不能继承
    /// 绑定类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class BindAttribute : Attribute
    {
        public Type BindType { get; set; }
        public BindAttribute(Type type)
        {
            this.BindType = type;
        }
    }
}
