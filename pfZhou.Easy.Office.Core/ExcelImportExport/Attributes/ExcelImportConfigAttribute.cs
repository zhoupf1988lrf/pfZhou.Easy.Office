using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Attributes
{
    /// <summary>
    /// Excel模板导入配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ExcelImportConfigAttribute : Attribute
    {
        public string BusinessName { get; private set; }
        public Type EntityType { get; private set; }
        public ExcelImportConfigAttribute(string businessName, Type entityType)
        {
            this.BusinessName = businessName;
            this.EntityType = entityType;
        }
    }
}
