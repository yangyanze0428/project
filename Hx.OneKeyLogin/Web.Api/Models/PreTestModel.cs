using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models
{
    public class PreTestModel
    {
        public string AppKey { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantNo { get; set; }

        public string AppSecret  { get; set; }

        /// <summary>
        /// 加密数据AES
        /// </summary>
        public string DataContent { get; set; }
        /// <summary>
        /// 签名 MD5
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
    }
}
