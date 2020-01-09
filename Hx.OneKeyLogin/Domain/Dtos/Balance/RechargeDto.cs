using Domain.Common.Enums;
using System;

namespace Domain.Common.Dtos.Balance
{
    public class RechargeDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OperatorId { get; set; }
        /// <summary>
        /// 充值条数
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int Money { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public Bank Bank { get; set; }
        /// <summary>
        /// 充费模式
        /// </summary>
        public RechargeMode RechargeMode { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesMan { get; set; }
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
        public IdenTity IdenTity { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public TradeType TradeType { get; set; }
        /// <summary>
        /// 交易结果
        /// </summary>
        public TradeResult TradeResult { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string TranId { get; set; }
    }

    public class RechargePara : PageBase
    {
        public string UserId { get; set; }
        /// <summary>
        /// 标识 客户端充值 管理端充值
        /// </summary>
        public IdenTity IdenTity { get; set; }
        public TradeType TradeType { get; set; }
        /// <summary>
        /// 交易结果
        /// </summary>
        public TradeResult TradeResult { get; set; }
        public Bank Bank { get; set; }
        /// <summary>
        /// 充费模式
        /// </summary>
        public RechargeMode RechargeMode { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesMan { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
