using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("app")]
    public class AppModel : ViewModelBase
    {
        [View(Title = "Appkey")]
        public string Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [View(Title = "应用名称")]
        public string AppName { get; set; }
        [View(Title = "应用密钥")]
        public string AppSecret { get; set; }
        [View(Title ="应用签名")]
        public string Signature { get; set; }
        [View(Title = "安卓包名")]
        public string AndroidId { get; set; }
        [View(Title = "BundleId")]
        public string BundleId { get; set; }
        [View(Title = "密钥")]
        public string MessageSecret { get; set; }
        /// <summary>
        /// 客户账号
        /// </summary>
        [View(Title = "客户账号")]
        public string UserId { get; set; }
        /// <summary>
        /// 应用图标
        /// </summary>
        [View(Title = "应用图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>
        [View(Title = "应用类型")]
        public string AppType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [View(Title = "状态",Hidden =true)]
        public int Status { get; set; }
        [View(Title ="业务员")]
        public string SalesMan { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [View(Title = "创建人",Hidden =true)]
        public string Creator { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [View(Title = "操作人")]
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [View(Title = "操作时间", Hidden = true)]
        public string OperateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
