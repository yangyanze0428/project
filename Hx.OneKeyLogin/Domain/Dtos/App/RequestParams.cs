using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.App
{
    public class RequestParams
    {
        public string appKey { get; set; }
        public string encryptType { get; set; } = "AES";
        public string dataType { get; set; } = "json";
        public string dataContent { get; set; }
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
    }
}
