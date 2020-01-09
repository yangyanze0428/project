using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos.App
{
    public class PreTestDto
    {
        public string AppKey { get; set; }

        public string MerchantNo { get; set; }

        public string AppSecret { get; set; }
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
