using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfZhou.Easy.Office.Entities
{
    /// <summary>
    /// 供货商
    /// </summary>
    [Table("bx_wms_suppliers")]
    public class SupplierEntity : Entity
    {
        /// <summary>
        /// 供应方编码
        /// </summary>
        [StringLength(32)]
        [Description("供应方编码")]
        public string SupplierCode { get; set; }
        /// <summary>
        /// 供应方名称
        /// </summary>
        [StringLength(32)]
        [Required]
        [Description("供应方名称")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("法人代表")]
        public string Corporation { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(128)]
        [Description("地址")]
        public string Address { get; set; }
        /// <summary>
        ///账号
        /// </summary>
        [StringLength(32)]
        [Description("账号")]
        public string Account { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        [StringLength(32)]
        [Description("税号")]
        public string Tariff { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        [StringLength(64)]
        [Description("开户行")]
        public string Bank { get; set; }

        /// <summary>
        /// 联系人1
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("联系人1")]
        public string Contacts1 { get; set; }

        /// <summary>
        /// 联系电话1
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("联系电话1")]
        public string Tel1 { get; set; }

        /// <summary>
        /// 联系人2
        /// </summary>
        [StringLength(32)]
        [Description("联系人2")]
        public string Contacts2 { get; set; }

        /// <summary>
        /// 联系电话2
        /// </summary>
        [StringLength(32)]
        [Description("联系电话2")]
        public string Tel2 { get; set; }
        /// <summary>
        /// 供应方开始时间
        /// </summary>
        [Description("供应方开始时间")]
        public DateTime SupplyBeginDate { get; set; }
        /// <summary>
        /// 供应截止时间
        /// </summary>
        [Description("供应截止时间")]
        public DateTime SupplyEndDate { get; set; }

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
