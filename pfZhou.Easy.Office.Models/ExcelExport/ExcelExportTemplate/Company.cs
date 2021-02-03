using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelExport.ExcelExportTemplate
{
    /// <summary>
    /// 企业
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string Leager { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string Range { get; set; }
    }
}
