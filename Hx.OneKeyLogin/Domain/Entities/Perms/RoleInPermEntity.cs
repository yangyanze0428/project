using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hx.Domain.Entities;

namespace Domain.Common.Entities.Perms
{
    public class RoleInPermEntity : AggregateRoot<long>
    {
        public string RoleId { get; set; }
        public string PermId { get; set; }
        public string Operation { get; set; }
    }
    public class PermInfo {
        /// <summary>
        /// 用户id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public string PermId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermName { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }
    }
}
