using Hx.Domain.Entities;

namespace Domain.Common.Dtos.Perms
{
    public class RoleDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
    }
}
