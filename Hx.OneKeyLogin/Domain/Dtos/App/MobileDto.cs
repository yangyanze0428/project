using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.App
{
    public class RespResult
    {
        public bool success { get; set; }
        /// <summary>
        /// 手机号码。运营商类型
        /// </summary>
        public Dictionary<string,string> result { get; set; }
        public string  errorCode { get; set; }
        public string errorMsg { get; set; }
    }
}
