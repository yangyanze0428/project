using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    public enum TradeType
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        AliPay =1,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WxPay = 2,

        /// <summary>
        /// 线下支付
        /// </summary>
        [Description("线下")]
        Offline = 3,

        /// <summary>
        /// 线上
        /// </summary>
        [Description("线上")]
        OnLine = 4,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 5
    }
}
