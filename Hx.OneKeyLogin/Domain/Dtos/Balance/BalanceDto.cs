using Domain.Common.Enums;
using Hx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.Balance
{
    public class BalanceDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OperatorId { get; set; }
        public int BeforeValue { get; set; }
        /// <summary>
        /// 条数
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesMan { get; set; }
        public Bank Bank { get; set; }
        /// <summary>
        /// 充费模式
        /// </summary>
        public RechargeMode RechargeMode { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 交易结果
        /// </summary>
        public TradeResult TradeResult { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public TradeType TradeType { get; set; }
        public IdenTity IdenTity { get; set; }
    }

    public class BalancePara:PageBase
    {
        public string UserId { get; set; }
        public string Creator { get; set; }
        public string SalesMan { get; set; }
    }
}
