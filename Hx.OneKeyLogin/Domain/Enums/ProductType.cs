using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum ProductType
    {
        [Description("一键登录")]
        OneKeyLogin=1,

        [Description("本机号码校验")]
        LocalNumber =2
    }
}
