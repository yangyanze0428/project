using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.Balance
{
    public class BillingDetailsDto
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
        public ChargeType ChargeType { get; set; }
        /// <summary>
        /// 计费
        /// </summary>
        public int Value { get; set; } = -1;
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public string SalesMan { get; set; }
    }

    public class BillingDetailsPara : PageBase
    {
        public string UserId { get; set; }
        public string AppKey { get; set; }
        public DateTime StartTime { get; set; }= Convert.ToDateTime(DateTime.Now.ToShortDateString());
        public DateTime EndTime { get; set; }
    }
    public class BillingDetailsStatis
    {
        public DateTime CreateDate { get; set; }
        public int TotalCount { get; set; }
        public string FormatDate { get; set; }
    }
}
