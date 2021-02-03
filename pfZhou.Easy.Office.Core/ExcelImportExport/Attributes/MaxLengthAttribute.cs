using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 最大长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLengthAttribute : BaseFilterAttribute
    {
        public int MaxLength { get; set; }
        public string ErrorMessage { get; set; }

        public MaxLengthAttribute(int maxlength)
        {
            this.MaxLength = maxlength;
            base.ErrorMsg = $"超出最大长度{this.MaxLength}";
        }
    }
}
