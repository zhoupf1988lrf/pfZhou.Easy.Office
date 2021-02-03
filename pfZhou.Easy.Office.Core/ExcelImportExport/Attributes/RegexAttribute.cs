using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 正则
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RegexAttribute : BaseFilterAttribute
    {
        public string Regex { get; set; }
        public RegexAttribute(string regex)
        {
            this.Regex = regex;
        }
    }
}
