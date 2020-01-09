using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("order")]
    public class OrderModel: ViewModelBase
    {
        [View(Title ="订单ID")]
        public string Id { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        [View(Title = "应用key")]
        public string AppKey { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        [View(Title = "订单名称")]
        public string Name { get; set; }
        /// <summary>
        /// 客户账号
        /// </summary>
        [View(Title = "客户账号")]
        public string UserId { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        [View(Title = "产品分类")]
        public string ProductType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [View(Title = "金额")]
        public decimal Money { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [View(Title = "订单类型")]
        public string OrderType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [View(Title = "状态")]
        public int Status { get; set; }
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
    }
}
