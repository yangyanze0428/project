using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum AppType
    {
        [Description("游戏类")]
        Game=1,
        [Description("金融、理财类")]
        Finance =2,
        [Description("购物商城类")]
        Shopping =4,
        [Description("通讯、社交类")]
        Social = 8,
        [Description("旅游酒店类")]
        Travel =16,
        [Description("视频音频类")]
        Video = 32,
        [Description("其他类")]
        Other = 64,
        [Description("实用工具类")]
        Tool = 128,
        [Description("新闻阅读类")]
        News = 256,
        [Description("摄影摄像类")]
        Camera = 512,
        [Description("学习教育类")]
        Studio = 1024,
        [Description("居家生活类")]
        Live = 2048,
        [Description("系统类")]
        Systems = 4096,
        [Description("效率办公类")]
        Work = 8192,
        [Description("医疗健康类")]
        Medical =16384,
        [Description("作弊软件类")]
        Cheat = 32768
    }
}
