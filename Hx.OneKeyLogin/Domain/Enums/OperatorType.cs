using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    /// <summary>
    /// 运营商类型移动1联通2电信3
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// 移动1
        /// </summary>
        [Description("移动")]
        CM = 1,
        /// <summary>
        /// 联通2
        /// </summary>
        [Description("联通")]
        CU = 2,
        /// <summary>
        /// 电信3
        /// </summary>
        [Description("电信")]
        CT = 3,
    }
}
