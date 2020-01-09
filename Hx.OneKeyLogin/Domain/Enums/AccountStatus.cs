using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum AccountStatus
    {
        [Description("正常")]
        Normal = 1,

        [Description("拒绝登录")]
        DenyLogin = 2,

        [Description("锁定")]
        Locked = 4,

        [Description("禁用")]
        Obsolete = 8
    }
}
