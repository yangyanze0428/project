using Domain.Common.Enums;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("auth")]
    public class AuthentyModel : ViewModelBase
    {
        [View(Title ="客户账号",Order =0)]
        public string Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [View(Title = "公司名称", Order = 1)]
        public string Name { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [View(Title = "营业执照", Order = 2)]
        public string Licence { get; set; }
        [View(Title ="业务员")]
        public string SalesMan { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [View(Title = "固话", Order = 6)]
        public string Telephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [View(Title = "公司地址", Order = 7)]
        public string Address { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [View(Title = "邮箱", Order = 5)]
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [View(Title = "手机号", Order = 4)]
        public string Phone { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间", Order = 8)]
        public string CreateDate { get; set; }
        [View(Hidden =true)]
        public AuditResult AuditResult { get; set; }
        [View(Title ="状态", Order = 3)]
        public string AuditResultDesc { get; set; }
    }
}
