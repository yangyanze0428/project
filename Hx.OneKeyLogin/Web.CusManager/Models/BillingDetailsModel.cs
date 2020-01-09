using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CusManager.Models
{
    [ModelKey("details")]
    public class BillingDetailsModel: ViewModelBase
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [View(Title = "客户账号")]
        public string UserId { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        [View(Title = "AppKey")]
        public string AppKey { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        [View(Title = "令牌")]
        public string Token { get; set; }
        /// <summary>
        /// 成功类型1预验成功2取号成功
        /// </summary>
        [View(Title = "计费类型")]
        public string ChargeType { get; set; }
        /// <summary>
        /// 计费
        /// </summary>
        [View(Title = "计费条数")]
        public int Value { get; set; }
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
    }
}
