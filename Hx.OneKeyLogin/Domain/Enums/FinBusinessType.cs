using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum FinBusinessType
    {
        /// <summary>
        /// 充值
        /// </summary>
        [Description("充值")]
        Recharge = 1,

        /// <summary>
        /// 子账户充值
        /// </summary>
        [Description("子账户充值")]
        SubRecharge = 2,

        /// <summary>
        /// 扣费
        /// </summary>
        [Description("扣费")]
        Charge = 4,

        /// <summary>
        /// 退费
        /// </summary>
        [Description("退费")]
        Refund = 8,
    }
}
