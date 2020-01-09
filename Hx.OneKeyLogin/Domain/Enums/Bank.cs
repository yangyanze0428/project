using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
   public enum Bank
    {
        /// <summary>
        /// 中国工商银行
        /// </summary>
        [Description("中国工商银行")]
        ICBC = 1,

        /// <summary>
        /// 中国建设银行
        /// </summary>
        [Description("中国建设银行")]
        CCB = 2,

        /// <summary>
        /// 中国农业银行
        /// </summary>
        [Description("中国农业银行")]
        ABC = 4,

        /// <summary>
        /// 中国银行
        /// </summary>
        [Description("中国银行")]
        BOC = 8,
        /// <summary>
        /// 中国邮政储蓄银行
        /// </summary>
        [Description("中国邮政储蓄银行")]
        PSBC = 16,

        /// <summary>
        /// 交通银行
        /// </summary>
        [Description("交通银行")]
        BCM = 32,
    }
}
