using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    /// <summary>
    /// 预付费，后付费
    /// </summary>
    public enum PayMode
    {
        /// <summary>
        /// 预付费
        /// </summary>
        [Description("预付费")]
        PrePay = 0,

        /// <summary>
        /// 后付费
        /// </summary>
        [Description("后付费")]
        PostPay = 1
    }
}
