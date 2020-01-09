using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common.Enums
{
    public enum TaskResultStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("未知")]
        Unknown = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        OK = 1,
        [Description("应用配置信息错误")]
        AppConfigNotError = 2,
        [Description("签名错误")]
        SignatureError = 3,
        [Description("请求失效")]
        RequestInvalid = 4,
        [Description("解析错误")]
        DecodeError = 5,
        [Description("频率限制")]
        ExceedRate = 6,
        [Description("运营商验证失败")]
        ChannelValidFail = 7,
        [Description("参数校验不通过")]
        ParamValidNotPass = 8,
        [Description("AppKey错误")]
        AppKeyError = 9,
        [Description("时间戳错误")]
        TimeStampError = 10,

        [Description("余额不足")]
        Arrears = 11,
        [Description("系统错误")]
        SystemError = 12,
        [Description("预验签名错误")]
        PreSignatureError = 13,
        [Description("令牌错误")]
        TokenError = 14,
        [Description("其它错误")]
        Other = 15

    }
}
