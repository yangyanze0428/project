using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("cus1")]
    public class cCustomerModel : ViewModelBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        [View(Title = "账号")]
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        //[View(Title = "姓名")]
        //public string Name { get; set; }

        [View(Title = "余额")]
        public int Balance { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [View(Title = "业务员")]
        public string SalesMan { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [View(Title = "邮箱")]
        public string BusinessEmail { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [View(Title = "手机号")]
        public string BusinessPhone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [View(Title = "联系人")]
        public string BusinessContact { get; set; }
        [View(Title = "QQ")]
        public string BusinessQq { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [View(Title = "操作人")]
        public string Operator { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [View(Title = "创建人")]
        public string Creator { get; set; }
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [View(Title = "备注")]
        public string Remark { get; set; }
    }

    [ModelKey("cus")]
    public class CustomerModel : ViewModelBase
    {
        [View(Title = "账号")]
        public string Id { get; set; }
        [View(Title = "余额")]
        public int Balance { get; set; }
        #region customer
        /// <summary>
        /// 业务联系人
        /// </summary>
        [View(Title = "业务联系人", Hidden = true)]
        public string BusinessContact { get; set; }
        /// <summary>
        /// 业务手机号       
        /// </summary>
        [View(Title = "业务手机号", Hidden = true)]
        public string BusinessPhone { get; set; }
        /// <summary>
        /// 业务邮箱
        /// </summary>
        [View(Title = "业务邮箱", Hidden = true)]
        public string BusinessEmail { get; set; }
        /// <summary>
        /// 业务邮箱
        /// </summary>
        [View(Hidden = true)]
        public AuditResult AuditResult {get;set;}
        //[View(Title = "客户状态")]
        //public string AuditResultDesc { get; set; }
        /// <summary>
        /// 业务qq
        /// </summary>
        [View(Title = "业务QQ", Hidden = true)]
        public string BusinessQq { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        [View(Title = "业务员")]
        public string SalesMan { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [View(Title = "操作人")]
        public string Operator { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [View(Title = "创建人")]
        public string Creator { get; set; }
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
        #endregion
        #region authenty
        /// <summary>
        /// 公司名称
        /// </summary>
        [View(Title = "公司名称")]
        public string Name { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [View(Title = "营业执照")]
        public string Licence { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [View(Title = "状态")]
        public string Status { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [View(Title = "电话", Hidden = true)]
        public string Telephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [View(Title = "地址", Hidden = true)]
        public string Address { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        [View(Title = "邮箱", Hidden = true)]
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [View(Title = "手机号", Hidden = true)]
        public string Phone { get; set; }
        [View(Title = "实名时间", Hidden = true)]
        public string AuthCreateDate { get; set; }
        #endregion
    }
}
