using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Expenses
{
    public class RechargeEntity : AggregateRoot<long>
    {
        public string UserId { get; set; }
       // public string AppKey { get; set; }
       // public string Mobile { get; set; }
        //public string Success { get; set; }
        //public string OperatorType { get; set; }
        //public string Description { get; set; }
        /// <summary>
        /// 充值条数
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int Money { get; set; }
        public int Bank { get; set; }
        /// <summary>
        /// 充费模式
        /// </summary>
        public int RechargeMode { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesMan { get; set; }
        public string OperatorId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 订单名称、产品名称
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 标识 客户端充值 管理端充值
        /// </summary>
        public int IdenTity { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int TradeType { get; set; }
        /// <summary>
        /// 交易结果
        /// </summary>
        public int TradeResult { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string TranId { get; set; }
    }
}
