using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum OrderType
    {
        [Description("成功")]
        Success=1,
        [Description("失败")]
        Fail =2,
        [Description("初始化")]
        Initial =4
    }
}
