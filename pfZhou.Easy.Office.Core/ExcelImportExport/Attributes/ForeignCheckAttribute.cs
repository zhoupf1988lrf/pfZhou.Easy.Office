using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 数据库外键引用的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ForeignCheckAttribute :BaseFilterAttribute
    {
        public Type FindClass { get; set; }
        public string FindProperty { get; set; }
        public string MapperProperty { get; set; }
        public ForeignCheckAttribute()
        {
            base.ErrorMsg = "在{0}外键表未查询到数据";
        }
    }
}
