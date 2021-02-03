using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Entities;
using pfZhou.Easy.Office.Models.ExcelImport;

namespace Zhou.Easy.Office.Models.ExcelImport.ExcelImportTemplate
{
    /// <summary>
    /// 货物信息导入类
    /// </summary>
    [ExcelImportConfig("goods", typeof(GoodsEntity))]
    public class GoodsTemplate : ExcelImportBase
    {
        /// <summary>
        /// 货物编号
        /// </summary>
        [Required]
        [ColName("货物编号")]
        //验证指定数据库的表的字段是否有重复的数据(唯一约束)
        [DBFieldDumplicate(ValidateClass = typeof(GoodsEntity), ValidateProperty = nameof(GoodsCode))]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        [Required]
        [ColName("货物名称")]
        //验证指定数据库的表的字段是否有重复的数据(唯一约束)
        [DBFieldDumplicate(ValidateClass = typeof(GoodsEntity), ValidateProperty = nameof(GoodsName))]
        public string GoodsName { get; set; } = string.Empty;
        /// <summary>
        /// 货物类别名称
        /// </summary>
        [ColName("货物类别"), Required]
        //验证指定数据库的表的字段是是否存在外键数据（外键数据查询）
        [ForeignCheck(FindClass = typeof(DictionaryEntity), FindProperty = nameof(DictionaryEntity.Name), MapperProperty = nameof(GcId))]
        public string GcName { get; set; } = string.Empty;
        /// <summary>
        /// 货物类别编号
        /// </summary>
        public string GcId
        {
            get; set;
        }
        /// <summary>
        /// 供应方名称
        /// </summary>
        [Required]
        [ColName("供应方名称")]
        //验证指定数据库的表的字段是是否存在外键数据（外键数据查询）
        [ForeignCheck(FindClass = typeof(SupplierEntity), FindProperty = nameof(SupplierEntity.SupplierName), MapperProperty = nameof(SupplierId))]
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 供应方编号
        /// </summary>
        public string SupplierId
        {
            get; set;
        }
        /// <summary>
        /// 价格
        /// </summary>
        [ColName("价格")]
        [Regex(@"^\d+(\.\d+)?$")]
        public double Price { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [ColName("规格")]
        public string Specs { get; set; } = string.Empty;
        /// <summary>
        /// 保质期
        /// </summary>
        [ColName("保质期")]
        [Regex(@"^\d+(\.\d+)?$")]
        public double ShelfLife { get; set; }
    }
}
