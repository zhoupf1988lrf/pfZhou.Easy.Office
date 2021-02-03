using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RangeAttribute : BaseFilterAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public RangeAttribute(int min, int max)
        {
            this.Min = min;
            this.Max = max;
            base.ErrorMsg = $"超过{min}~{max}";
        }
    }
}
