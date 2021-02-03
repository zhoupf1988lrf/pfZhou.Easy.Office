using pfZhou.Easy.Office.Core.ExcelExport.Attributes;

namespace pfZhou.Easy.Office.Models.ExcelExport.ExcelExportTemplate
{
    /// <summary>
    /// 签单情况
    /// </summary>
    [Title(FontColor = Core.ExcelExport.Enums.ColorEnum.RED, FontName = "微软雅黑", FontSize = 16,IsBold =true)]
    public class OrderStatus
    {
        /// <summary>
        /// 序号 
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 本日成功签单_预警
        /// </summary>
        public double TodaySuccessWaring { get; set; }
        /// <summary>
        /// 本日成功签单_续约
        /// </summary>
        public double TodaySuccessPaper { get; set; }
        /// <summary>
        /// 本日成功签单_流失
        /// </summary>
        public double TodaySuccessLose { get; set; }
        /// <summary>
        /// 本日成功签单_流失
        /// </summary>
        public double TodaySuccessSum { get; set; }
        /// <summary>
        /// 累计成功签约单数_预警
        /// </summary>
        public double TotalSuccessWaring { get; set; }
        /// <summary>
        /// 累计成功签约单数_续约
        /// </summary>
        public double TotalSuccessPaper { get; set; }
        /// <summary>
        /// 累计成功签约单数_丢失
        /// </summary>
        public double TotalSuccessLose { get; set; }
        /// <summary>
        /// 累计成功签约单数_合计
        /// </summary>
        public double TotalSuccessSum { get; set; }
        /// <summary>
        /// 任务数
        /// </summary>
        public double TaskSum { get; set; }
        /// <summary>
        /// 完成比例
        /// </summary>
        public string CompletePipe { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public string Order { get; set; }
    }
}
