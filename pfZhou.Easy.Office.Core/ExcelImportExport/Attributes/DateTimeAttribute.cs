using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 日期
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DateTimeAttribute : BaseFilterAttribute
    {

    }
}
