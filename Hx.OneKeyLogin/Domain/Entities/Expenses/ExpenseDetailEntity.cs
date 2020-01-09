using System;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Expenses
{
    public class ExpenseDetailEntity: AggregateRoot<long>
    {
        public string UserId { get; set; }
        public string Appkey{ get; set; }
        public string Mobile { get; set; }
        public int Success { get; set; }
        public int Value { get; set; } = -1;
        /// <summary>
        /// 运营商类型
        /// </summary>
        public int OperatorType { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public string SalesMan { get; set; }
    }
}
