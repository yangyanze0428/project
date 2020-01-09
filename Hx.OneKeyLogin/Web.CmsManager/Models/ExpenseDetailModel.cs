using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("expense")]
    public class ExpenseDetailModel : ViewModelBase
    {
        [View(Title = "客户编号")]
        public string UserId { get; set; }
        [View(Title = "AppKey")]
        public string AppKey { get; set; }
        [View(Title = "手机号")]
        public string Mobile { get; set; }
        /// <summary>
        /// 获取成功失败
        /// </summary>
        [View(Title = "获取成功失败")]
        public string Success { get; set; }
        [View(Title ="计费条数",Hidden =true)]
        public int Value { get; set; }
        /// <summary>
        /// 运营商类型
        /// </summary>
        [View(Title = "运营商类型")]
        public string OperatorType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [View(Title = "描述",Hidden =true)]
        public string Description { get; set; }
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
        [View(Title = "备注",Hidden =true)]
        public string Remark { get; set; }
    }
}
