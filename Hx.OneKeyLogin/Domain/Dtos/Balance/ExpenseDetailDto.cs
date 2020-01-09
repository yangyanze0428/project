using Domain.Common.Enums;
using System;

namespace Domain.Common.Dtos.Balance
{
    public class ExpenseDetailDto
    {
        public string UserId { get; set; }
        public string Appkey { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 计费条数
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 获取成功失败
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 运营商类型
        /// </summary>
        public OperatorType OperatorType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
        public string SalesMan { get; set; }
    }

    public class ExpenseDetailPara : PageBase
    {
        public string UserId { get; set; }
        public string Creator { get; set; }
        public string Mobile { get; set; }
        public string SalesMan { get; set; }
        public DateTime StartTime { get; set; }= Convert.ToDateTime(DateTime.Now.ToShortDateString());
        public DateTime EndTime { get; set; }
        public OperatorType OperatorType { get; set; }
    }

    public class ExpenseStatis
    {
        public DateTime CreateDate { get; set; }
        public int TotalCount { get; set; }
        public string FormatDate { get; set; }
    }
}
