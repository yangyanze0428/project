using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.MemberShip
{
    /// <summary>
    /// 员工
    /// </summary>
    public class StaffEntity : AggregateRootWithStringKey,IHaveCreateDate
    {
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
        public int UserType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public int OrgId { get; set; }
    }
}
