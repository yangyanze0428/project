using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum UserType
    {
        [Description("管理员")]
        Admin = 1,

        [Description("财务")]
        FinManager = 2,

        [Description("客服")]
        CustomerService = 4,

        [Description("销售员")]
        SalesPerson = 8,
        [Description("普通管理员")]
        SalesManager = 16
    }
}
