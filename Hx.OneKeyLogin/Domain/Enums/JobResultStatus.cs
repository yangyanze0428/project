using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum JobResultStatus
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

        /// <summary>
        /// 表示过程已结束
        /// </summary>
        [Description("过程结束")]
        Completed = 1001,

        /// <summary>
        /// 身份验证失败
        /// </summary>
        [Description("身份验证失败")]
        AuthFail = 2001,

        /// <summary>
        /// 需要提供有效身份验证参数
        /// </summary>
        [Description("需要提供有效身份验证参数")]
        AuthParaRequire = 2002,
        /// <summary>
        /// 令牌过期
        /// </summary>
        [Description("令牌过期")]
        TokenExpired = 2003,

        /// <summary>
        /// 无效令牌
        /// </summary>
        [Description("无效令牌")]
        InvalidToken = 2004,

        /// <summary>
        /// 令牌签名验证失败
        /// </summary>
        [Description("令牌签名验证失败")]
        ParaSignFail = 2005,

        /// <summary>
        /// IP非法
        /// </summary>
        [Description("IP非法")]
        InvalidIP = 2006,

        /// <summary>
        /// 无效账号
        /// </summary>
        [Description("无效账号")]
        InvalidAccount = 2007,

        /// <summary>
        /// 错误平台号
        /// </summary>
        [Description("错误平台号")]
        InvalidTId = 2008,


        /// <summary>
        /// 非法参数
        /// </summary>
        [Description("非法参数")]
        InvalidPara = 2009,

        /// <summary>
        /// 参数必须
        /// </summary>
        [Description("参数必须")]
        ParaRequire = 2010,
        /// <summary>
        /// 初始量
        /// </summary>
        [Description("初始量不足")]
        Initial = 2011,


        /// <summary>
        /// 短信内容超长
        /// </summary>
        [Description("短信内容超长")]
        ContentTooLong = 3001,

        /// <summary>
        /// 短信内容不能空
        /// </summary>
        [Description("内容空")]
        ContentEmpty = 3002,

        /// <summary>
        /// 短信内容不合法
        /// </summary>
        [Description("内容不合法")]
        InvalidContent = 3003,

        /// <summary>
        /// 短信内容包含敏感词
        /// </summary>
        [Description("敏感词")]
        SensitiveWord = 3004,

        /// <summary>
        /// 错误的手机号
        /// </summary>
        [Description("错误的手机号")]
        InvalidSn = 3101,

        /// <summary>
        /// 无目标手机号
        /// </summary>
        [Description("无目标手机号")]
        SnEmpty = 3102,

        /// <summary>
        /// 号码被限制
        /// </summary>
        [Description("号码被限制")]
        LimitedSn = 3103,

        /// <summary>
        /// 号码被锁定
        /// </summary>
        [Description("号码被锁定")]
        BlockedSn = 3104,

        /// <summary>
        /// 扩展号格式错误
        /// </summary>
        [Description("扩展号格式错误")]
        InvalidExtnumber = 3201,

        /// <summary>
        /// 扩展号超长
        /// </summary>
        [Description("扩展号超长")]
        ExtnumberTooLong = 3202,

        /// <summary>
        /// 需要签名
        /// </summary>
        [Description("需要签名")]
        SignRequire = 3301,

        /// <summary>
        /// 签名不匹配
        /// </summary>
        [Description("签名不匹配")]
        SignNotMatch = 3302,


        /// <summary>
        /// 通道运行状态错误
        /// </summary>
        [Description("通道运行状态错误")]
        ChannelError = 3401,

        /// <summary>
        /// 通道提交失败(提交超时或提交成功后返回错误并且无网关jobid)
        /// </summary>
        [Description("通道提交失败")]
        ChannelSubmitError = 3402,

        ///// <summary>
        ///// 通道连接超时
        ///// </summary>
        //[Description("通道连接超时")]
        //ChannelTimeoutError,

        /// <summary>
        /// 通道身份验证失败
        /// </summary>
        [Description("通道身份验证失败")]
        ChannelAuthFaild = 3403,

        /// <summary>
        /// 通道余额不足
        /// </summary>
        [Description("通道余额不足")]
        ChannelBalanceInsufficient = 3404,

        /// <summary>
        /// 通道下发失败
        /// </summary>
        [Description("通道下发失败")]
        ChannelMtError = 3405,

        /// <summary>
        /// 无可用通道
        /// </summary>
        [Description("无可用通道")]
        NoUseableChannel = 3406,

        /// <summary>
        /// 通道状态报告失败
        /// </summary>
        [Description("状态失败")]
        ChannelReportFail = 3407,

        /// <summary>
        /// 需要审核
        /// </summary>
        [Description("需要审核")]
        AuditNeeded = 3501,

        /// <summary>
        /// 余额不足
        /// </summary>
        [Description("余额不足")]
        BalanceInsufficient = 3601,

        /// <summary>
        /// State值超长，最大长度为64
        /// </summary>
        [Description("State值超长")]
        StateTooLong = 3701,

        /// <summary>
        /// 计费错误
        /// </summary>
        [Description("计费错误")]
        ChangingError = 3801,


        /// <summary>
        /// 提交速度超过限制
        /// </summary>
        [Description("提交速度超过限制")]
        RateLimit = 4001,

        /// <summary>
        /// 超过系统设定的时间误差范围
        /// </summary>
        [Description("时间误差超限")]
        AteLimit = 4002,



        /// <summary>
        /// 不支持的操作
        /// </summary>
        [Description("不支持的操作")]
        UnsupportedOperation = 9001,

        /// <summary>
        /// 系统异常
        /// </summary>
        [Description("系统异常")]
        Exception = 9002,

        /// <summary>
        /// 安全检测失败
        /// </summary>
        [Description("安全检测失败")]
        SafeCheckFailure = 9003,
        [Description("超时")]
        TimeOut = 9004,
        /// <summary>
        /// 其它错误
        /// </summary>
        [Description("其它错误")]
        Other = 9999

    }
}
