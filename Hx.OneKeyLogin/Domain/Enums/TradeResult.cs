using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    public enum TradeResult
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        Wait =1,

        /// <summary>
        /// 支付成功
        /// </summary>
        [Description("支付成功")]
        Success = 2,

        /// <summary>
        /// 支付失败
        /// </summary>
        [Description("支付失败")]
        Fail = 3,

        /// <summary>
        /// 交易取消
        /// </summary>
        [Description("交易取消")]
        Cancel = 4,

        /// <summary>
        /// 无效账单
        /// </summary>
        [Description("无效账单")]
        Invalid = 5
    }
}
