using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfZhou.Easy.Office.Entities
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [Table("bx_wms_dictionary")]
    public class DictionaryEntity : Entity
    {
        /// <summary>
        /// 父级编号
        /// </summary>
        [StringLength(32)]
        [Description("父级编号")]
        public string ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(64)]
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(64)]
        [Description("编号")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Description("状态(0启用1禁用)")]
        public override Status Status { get; set; }

        /// <summary>
        /// 是否删除(0未删除1已删除)
        /// </summary>
        [Description("是否删除(0未删除1已删除)")]
        public override DeleteStatus IsDelete { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(32)]
        [Description("创建人")]
        public override string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public override DateTime CreateDate { get; set; }
        /// <summary>
        /// 编辑人
        /// </summary>
        [StringLength(32)]
        [Description("编辑人")]
        public override string Updator { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        [Description("编辑时间")]
        public override DateTime UpdateDate { get; set; }
    }
}
