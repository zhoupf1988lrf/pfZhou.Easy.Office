using System;
using System.Collections.Generic;
using System.Text;

namespace pfZhou.Easy.Office.Models.ExcelExport.ExcelExportTemplate
{
    /// <summary>
    /// 存款情况
    /// </summary>
    public class Ckqd
    {
        /// <summary>
        /// 等级
        /// </summary>
        public string DJ { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 上期结余_件数
        /// </summary>
        public int PrevJyJianShu { get; set; }
        /// <summary>
        /// 上期结余_重量
        /// </summary>
        public double PrevJyWeight { get; set; }
        /// <summary>
        /// 上期结余_比例
        /// </summary>
        public string PrevPipe { get; set; }
        /// <summary>
        /// 本期调入_件数
        /// </summary>
        public int NowDrJianShu { get; set; }
        /// <summary>
        /// 本期调入_重量
        /// </summary>
        public double NowDrWeight { get; set; }
        /// <summary>
        /// 本期调入_比例
        /// </summary>
        public string NowDrPipe { get; set; }
        /// <summary>
        /// 本期发出_车间投料_件数
        /// </summary>
        public int NowSendCjtlJianshu { get; set; }
        /// <summary>
        /// 本期发出_车间投料_重量
        /// </summary>
        public double NowSendCjtlWeight { get; set; }
        /// <summary>
        /// 本期发出_车间投料_比例
        /// </summary>
        public string NowSendCjtlPipe { get; set; }
        /// <summary>
        /// 本期发出_产品外销百分比_件数
        /// </summary>
        public int NowSendProductJianShu { get; set; }
        /// <summary>
        /// 本期发出_产品外销百分比_重量
        /// </summary>
        public double NowSendProductWeight { get; set; }
        /// <summary>
        /// 本期发出_产品外销百分比_比例
        /// </summary>
        public string NowSendProductPipe { get; set; }
        /// <summary>
        /// 平均值
        /// </summary>
        public double Avg { get; set; }
    }
}
