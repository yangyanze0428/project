using Domain.Common.Dtos.MemberShip;
using Domain.Common.Dtos.Perms;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    public class LoginUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PayMode PayMethod { get; set; }
        public UserType Role { get; set; }
        public OrgDto Org { get; set; }
    }
    /// <summary>
    /// 员工显示
    /// </summary>
    [ModelKey("staff")]
    public class StaffModel : ViewModelBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        [View(Title = "账号")]
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [View(Title = "姓名")]
        public string Name { get; set; }
        [View(Title = "员工类型")]
        public string UserType { get; set; }
        [View(Title = "部门")]
        public string OrgIdDesc { get; set; }
        [View(Hidden = true)]
        public int OrgId { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [View(Title = "手机号")]
        public string Phone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [View(Title = "状态")]
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
