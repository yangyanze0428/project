using Domain.Common.Enums;
using System;

namespace Web.CusManager.Models
{
    [ModelKey("order")]
    public class OrderModel : ViewModelBase
    {
        [View(Title ="订单号",Order =0)]
        public string Id { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public ProductType ProductType { get; set; }
        [View(Title ="产品类型", Order = 3)]
        public string ProductTypeDesc { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [View(Title ="交易金额", Order = 4)]
        public decimal Money { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrderType { get; set; }
        [View(Title ="订单类型", Order = 5)]
        public string OrderTypeDesc { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间", Order = 6)]
        public string CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
