using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelExport.ExcelExportTemplate
{
    /// <summary>
    /// 顺序必须保持一致
    /// </summary>
    public class JsonFileConfig
    {
        /// <summary>
        /// 属性用 特殊字符隔开
        /// </summary>
        public string PropertyNames { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// sheet名称
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// header内容
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
    }
}
