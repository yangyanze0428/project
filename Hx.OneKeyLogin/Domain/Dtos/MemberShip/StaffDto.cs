using System;
using Domain.Common.Enums;
using Hx.Domain.Entities;

namespace Domain.Common.Dtos.MemberShip
{
    /// <summary>
    /// 员工
    /// </summary>
    public class StaffDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public int OrgId { get; set; }
    }
    public class StaffPara : PageBase
    {
        public string Id { get; set; }


        public string RealName { get; set; }
    }
}
