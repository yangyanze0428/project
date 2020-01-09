using Hx.Domain.Entities;

namespace Domain.Common.Entities.Perms
{
    public class RoleEntity : AggregateRootWithStringKey
    {
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
