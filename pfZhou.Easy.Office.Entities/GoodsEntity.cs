using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfZhou.Easy.Office.Entities
{
    /// <summary>
    /// 货物信息
    /// </summary>
    [Table("bx_wms_goods")]
    public class GoodsEntity : Entity
    {
        /// <summary>
        /// 货物类别
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("货物类别")]
        public string GcId { get; set; }
        /// <summary>
        /// 货物编号
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("货物编号")]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Description("货物名称")]
        public string GoodsName { get; set; }
        /// <summary>
        /// 内包装数量
        /// </summary>
        [Description("内包装数量")]
        public double InnerPackingQuantity { get; set; }
        /// <summary>
        /// 内包装货物体积
        /// </summary>
        [Description("内包装货物体积")]
        public double InnerPackingVolume { get; set; }
        /// <summary>
        /// 内包装货物重量
        /// </summary>
        [Description("内包装货物重量")]
        public double InnerPackingWeight { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        [StringLength(32)]
        [Description("材质")]
        public string Material { get; set; }
        /// <summary>
        /// 最大库存量
        /// </summary>
        [Description("最大库存量")]
        public double MaxStock { get; set; }
        /// <summary>
        /// 最小库存量
        /// </summary>
        [Description("最小库存量")]
        public double MinStock { get; set; }
        /// <summary>
        /// 外包装数量
        /// </summary>
        [Description("外包装数量")]
        public double OuterPackingQuantity { get; set; }
        /// <summary>
        /// 外包装货物体积
        /// </summary>
        [Description("外包装货物体积")]
        public double OuterPackingVolume { get; set; }
        /// <summary>
        /// 外包装货物重量
        /// </summary>
        [Description("外包装货物重量")]
        public double OuterPackingWeight { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Description("价格")]
        public double Price { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        [Description("重量")]
        public double Weight { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        [Description("保质期")]
        public double ShelfLife { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("规格")]
        public string Specs { get; set; }
        /// <summary>
        /// 供应方编号
        /// </summary>
        [Required]
        [StringLength(32)]
        [Description("供应方编号")]
        public string SupplierId { get; set; }

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
