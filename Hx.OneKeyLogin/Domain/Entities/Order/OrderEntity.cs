using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Order
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderEntity : AggregateRootWithStringKey, IHaveCreateDate
    {
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
        public int ProductType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
