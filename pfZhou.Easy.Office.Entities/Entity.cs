using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pfZhou.Easy.Office.Entities
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    public abstract class Entity 
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [Required]
        [StringLength(32)]
        [Description("编号")]
        public virtual string Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual Status Status { get; set; }

        /// <summary>
        /// 是否删除(0未删除1已删除)
        /// </summary>
        public virtual DeleteStatus IsDelete { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        [StringLength(32)]
        public virtual string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 编辑人id
        /// </summary>
        [StringLength(32)]
        public virtual string Updator { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime UpdateDate { get; set; }
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 0,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 1
    }
    /// <summary>
    /// 删除状态
    /// </summary>
    public enum DeleteStatus
    {
        /// <summary>
        /// 正常 未删除
        /// </summary>
        [Description("正常")]
        Normal,
        /// <summary>
        /// 非正常 已删除
        /// </summary>
        [Description("已删除")]
        HasDelete
    }
}
