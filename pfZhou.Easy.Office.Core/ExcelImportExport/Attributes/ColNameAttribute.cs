using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ColNameAttribute : Attribute
    {
        public string ColName { get; set; }
        public ColNameAttribute(string colname)
        {
            this.ColName = colname;
        }
    }
}
