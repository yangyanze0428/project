using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum AuditResult
    {
        [Description("待审核")]
        Pending = 1,

        [Description("审核通过")]
        Ok = 2,

        [Description("审核拒绝")]
        Fail = 4,
    }
}
