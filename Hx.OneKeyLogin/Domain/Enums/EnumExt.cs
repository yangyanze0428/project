using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Enums
{
    public static class EnumExt
    {
        public static string ToIntString(this TaskResultStatus status)
        {
            return ((int)status).ToString();
        }
    }
}
