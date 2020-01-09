using Hx.Domain.Entities;

namespace Domain.Common.Dtos.Perms
{
    public class UserInRoleDto
    {
        public int Id { get; set; }
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
