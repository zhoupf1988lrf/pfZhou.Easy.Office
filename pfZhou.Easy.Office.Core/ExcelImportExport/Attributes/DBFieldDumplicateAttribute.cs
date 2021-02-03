using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// 数据库表 字段的重复特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DBFieldDumplicateAttribute : BaseFilterAttribute
    {
        public Type ValidateClass { get; set; }

        public string ValidateProperty { get; set; }
        public DBFieldDumplicateAttribute()
        {
            base.ErrorMsg = "在{0}表已存在";
        }
    }
}
