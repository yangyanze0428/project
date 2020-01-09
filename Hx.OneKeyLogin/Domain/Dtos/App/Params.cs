using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.App
{
    public class Params
    {
        public string AppKey { get; set; }
        public string DataContent { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }

    }
}
