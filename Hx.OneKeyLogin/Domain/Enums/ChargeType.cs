using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{

    /// <summary>
    /// 计费类型
    /// </summary>
    public enum ChargeType
    {
        /// <summary>
        /// 预验成功
        /// </summary>
        [Description("预验成功")]
        PreSucc = 1,
        /// <summary>
        /// 取号成功
        /// </summary>
        [Description("取号成功")]
        CompleSucc = 2
    }
}
