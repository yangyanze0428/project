using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models
{
    public class Parameter:IRequest<bool>
    {
        /// <summary>
        /// 应用唯一标识
        /// </summary>
        public string Appkey { get; set; }
        /// <summary>
        /// 加密数据
        /// </summary>
        public string DataContent { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// guid 
        /// </summary>
        public string Nonce { get; set; }

    }
}
