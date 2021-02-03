using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using pfZhou.Easy.Office.Core.ExcelExport.Enums;

namespace pfZhou.Easy.Office.Core.ExcelExport.Attributes
{
    /// <summary>
    /// 标题装饰器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TitleAttribute : BaseDecorationAttribute
    {
        /// <summary>
        /// 字体颜色
        /// </summary>
        public ColorEnum FontColor { get; set; } = ColorEnum.BLACK;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; } = 10;
        /// <summary>
        /// 字体:
        /// 楷体、微软雅黑
        /// </summary>
        public string FontName { get; set; } = "宋体";
        /// <summary>
        /// 是否加粗
        /// </summary>
        public bool IsBold { get; set; }
        /// <summary>
        /// 水平对齐
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Center;


        /// <summary>
        /// 背景色
        /// </summary>
        public short FillForegroundColor { get; set; } = HSSFColor.White.Index;
        /// <summary>
        /// 背景色填充规则
        /// </summary>
        public FillPattern FillPattern { get; set; } = FillPattern.SolidForeground;

        /// <summary>
        /// 底部边框样式
        /// </summary>

        public BorderStyle BorderBottom { get; set; } = BorderStyle.Thin;
        /// <summary>
        /// 顶部边框样式
        /// </summary>
        public BorderStyle BorderTop { get; set; } = BorderStyle.Thin;
        /// <summary>
        /// 左部边框样式
        /// </summary>
        public BorderStyle BorderLeft { get; set; } = BorderStyle.Thin;
        /// <summary>
        /// 右部边框样式
        /// </summary>
        public BorderStyle BorderRight { get; set; } = BorderStyle.Thin;
    }
}
