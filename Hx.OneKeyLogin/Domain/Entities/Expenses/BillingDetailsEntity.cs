using Hx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entities.Expenses
{
    /// <summary>
    /// 计费明细
    /// </summary>
  public  class BillingDetailsEntity : AggregateRoot<long>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 成功类型1预验成功2取号成功
        /// </summary>
        public int ChargeType { get; set; }
        /// <summary>
        /// 计费
        /// </summary>
        public int Value { get; set; } = -1;
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public string SalesMan { get; set; }
    }
}
