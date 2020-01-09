using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    /// <summary>
    /// 充值模式
    /// </summary>
    public enum RechargeMode
    {
        [Description("首充")]
        FirstCharge = 1,

        [Description("续充")]
        ContinuedCharge = 2
    }
}
