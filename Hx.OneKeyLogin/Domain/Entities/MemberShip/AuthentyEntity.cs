using System;
using System.Collections.Generic;
using System.Text;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.MemberShip
{
    public class AuthentyEntity : AggregateRootWithStringKey, IHaveCreateDate
    {
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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        public string SalesMan { get; set; }
    }
}
