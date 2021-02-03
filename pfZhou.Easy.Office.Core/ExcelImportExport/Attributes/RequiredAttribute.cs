using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 必填
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : BaseFilterAttribute
    {
        public RequiredAttribute()
        {
            base.ErrorMsg = "必填";
        }
    }
}
