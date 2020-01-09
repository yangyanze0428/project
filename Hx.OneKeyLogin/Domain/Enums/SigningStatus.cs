using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum SigningStatus
    {
        [Description("正常")]
        Normal=1,
        [Description("停用")]
        Stop=2
    }
}
