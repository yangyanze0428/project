using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum DataType
    {
        [Description("应用")]
        App = 1,
        [Description("企业认证")]
        Authenty = 2,
        [Description("客户")]
        Customer = 4,
        [Description("交易明细")]
        ExpenseDetail = 8,
        [Description("充值")]
        Recharge = 16,
        [Description("余额")]
        Balance = 32,
        [Description("员工")]
        Staff = 64,
        [Description("计费明细")]
        BillingDetails = 128,
        [Description("产品列表")]
        Prod = 136
    }
}
