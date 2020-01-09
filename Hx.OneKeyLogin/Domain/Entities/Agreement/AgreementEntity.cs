using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Agreement
{
    /// <summary>
    /// 收费协议
    /// </summary>
    public class AgreementEntity : AggregateRootWithStringKey, IHaveCreateDate
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int ProductType { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary>
        public int MoneyType { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 签约状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime InvalidDate { get; set; }
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
    }
}
