using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CusManager.Models
{
    [ModelKey("expense")]
    public class ExpenseDetailModel : ViewModelBase
    {
        [View(Title = "手机号")]
        public string Mobile { get; set; }
        /// <summary>
        /// 获取成功失败
        /// </summary>
        public string Success { get; set; }
        [View(Title = "获取成功失败")]
        public string  SuccessFormat { get; set; }
        /// <summary>
        /// 运营商类型
        /// </summary>
        public OperatorType OperatorType { get; set; }
        [View(Title = "运营商类型")]
        public string OperatorTypeDesc { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [View(Title = "描述")]
        public string Description { get; set; }
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
