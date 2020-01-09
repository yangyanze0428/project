using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("recharge")]
    public class RechargeModel: ViewModelBase
    {
        [View(Title ="ID",Hidden =true)]
        public string Id { get; set; }
        [View(Title = "客户编号")]
        public string UserId { get; set; }
        //[View(Title = "单价")]
        //public decimal Price { get; set; }
        [View(Title = "交易号")]
        public string TranId { get; set; }
       
        [View(Title = "充值条数")]
        public int Value { get; set; }
        [View(Title = "充值金额")]
        public int Money { get; set; }
        [View(Title = "银行")]
        public string Bank { get; set; }
        [View(Title = "操作人")]
        public string OperatorId { get; set; }
        /// <summary>
        /// 充费模式
        /// </summary>
        [View(Title = "充费模式")]
        public string RechargeMode { get; set; }
        [View(Title = "交易结果")]
        public string TradeResult { get; set; }
        [View(Title = "支付方式")]
        public string TradeType { get; set; }
        [View(Title = "订单号")]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 订单名称、产品名称
        /// </summary>
        [View(Title = "订单名称")]
        public string OrderName { get; set; }
        /// <summary>
        /// 标识 客户端充值 管理端充值
        /// </summary>
        [View(Title = "标识")]
        public string IdenTity { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        [View(Title = "业务员")]
        public string SalesMan { get; set; }
        [View(Title = "充值时间")]
        public string CreateDate { get; set; }
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
