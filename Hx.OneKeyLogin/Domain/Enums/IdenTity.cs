using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    public enum IdenTity
    {
        [Description("客户端购买")]
        Client = 1,
        [Description("管理端充值")]
        Server = 2
    }
}
