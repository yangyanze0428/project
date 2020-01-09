using System;
using System.Collections.Generic;
using System.Text;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Order
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProdEntity : AggregateRootWithStringKey
    {
        //public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 数量/条数
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public string UserId { get; set; }
        public string SalesMan { get; set; }
    }
}
