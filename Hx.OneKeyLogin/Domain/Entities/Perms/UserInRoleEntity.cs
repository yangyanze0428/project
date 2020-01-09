using Hx.Domain.Entities;

namespace Domain.Common.Entities.Perms
{
    public class UserInRoleEntity :AggregateRoot<long> //AggregateRootWithStringKey
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

    }
}
