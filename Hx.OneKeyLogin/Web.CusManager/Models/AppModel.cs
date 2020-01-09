using Domain.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CusManager.Models
{
    [ModelKey("app")]
    public class AppModel:ViewModelBase
    {
        [View(Title = "AppId",Order = 0)]
        public string Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [View(Title = "应用名称", Order = 1)]
        public string AppName { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 应用图标
        /// </summary>
        [View(Title = "应用图标", Order = 5)]
        public string Icon { get; set; }
        public string AppKey { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>
        public AppType AppType { get; set; }
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
        public string OperateDate { get; set; }
        [View(Title = "安卓包名", Order = 8)]
        public string AndroidId { get; set; }
        [View(Title = "BundleId", Order = 9)]
        public string BundleId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间", Order = 10)]
        public string CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
