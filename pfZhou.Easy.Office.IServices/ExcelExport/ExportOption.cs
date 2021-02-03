using pfZhou.Easy.Office.Core.ExcelExport.Enums;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.IServices.ExcelExport
{
    /// <summary>
    /// 导出项
    /// </summary>
    public class ExportOption
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 工作簿名称
        /// </summary>
        public string SheetName{ get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 表头格式
        /// 相邻父列头间用('#')分割，父列头与子列头间用(' ')分割，相邻子列头间用(',')分割
        /// eq:
        ///     两行：序号#分公司#组别#本日成功签约单数 预警,续约,流失,合计#累计成功签约单数 预警,续约,流失,合计#任务数#完成比例#排名 
        ///     多行也是同样的方式
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 属性名称集合
        /// </summary>
        public List<string> PropertyNames { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ExcelType ExcelType { get; set; } = ExcelType.XLS;
        /// <summary>
        /// Excel导出驱动
        /// </summary>
        public IExcelExportProvider ExcelExportProvider { get; set; }
    }
}
