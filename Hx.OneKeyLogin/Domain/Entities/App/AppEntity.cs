using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Application
{
    /// <summary>
    /// 应用
    /// </summary>
    public class AppEntity : AggregateRootWithStringKey, IHaveCreateDate
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        public string AppSecret { get; set; }
        public string AndroidId { get; set; }
        public string BundleId { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 应用图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>
        public int AppType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateDate { get; set; }
        /// <summary>
        /// 和下游通道交换的加密密匙
        /// </summary>
        public string MessageSecret { get; set; }
        public string MsgSecret { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string SalesMan { get; set; }
        /// <summary>
        /// 应用签名
        /// </summary>
        public string Signature { get; set; }
    }
}
