using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CusManager.Models
{
    public class RechargeModel
    {
        public string OrderNumber { get; set; }
        public string OrderName { get; set; }
        public string TradeType { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string IdenTity { get; set; }
        public string TradeResult { get; set; }
        public string UserId { get; set; }
        public int Money { get; set; }
        public int Value { get; set; }
        public string CreateDate { get; set; }
        public string TranId { get; set; }
    }
}
