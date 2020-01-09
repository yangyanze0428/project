using Hx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entities.Balance
{
    public class BalanceEntity : AggregateRoot<long>
    {
        public string UserId { get; set; }
        public string OperatorId { get; set; }
        public int BeforeValue { get; set; }
        public int Value { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Remark { get; set; }

        public string SalesMan { get; set; }
    }
}
