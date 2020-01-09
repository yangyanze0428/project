using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Dtos
{
    public class PageBase
    {
        /// <summary>
        /// 当前页数 从0开始
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        
        public UserType RoleId { get; set; }
        public int OrgId { get; set; }
    }
}
