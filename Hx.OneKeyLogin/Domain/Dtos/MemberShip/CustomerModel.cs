using System;
using Domain.Common.Enums;

namespace Domain.Common.Dtos.MemberShip
{
    public class CustomerModel
    {
        public string Id { get; set; }
        #region customer
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 付费类型
        /// </summary>
        public PayMode PayMode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }

        public AuditResult AuditResult { get; set; }

        public string PermsId { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string SalesMan { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 业务联系人
        /// </summary>
        public string BusinessContact { get; set; }
        /// <summary>
        /// 业务手机号       
        /// </summary>
        public string BusinessPhone { get; set; }
        /// <summary>
        /// 业务邮箱
        /// </summary>
        public string BusinessEmail { get; set; }
        /// <summary>
        /// 业务qq
        /// </summary>
        public string BusinessQq { get; set; }
        #endregion
        #region authenty
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string Licence { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        public DateTime AuthCreateDate { get; set; }
        #endregion
    }

    public class CustomPara : PageBase
    {
        public string UserId { get; set; }
    }
}
