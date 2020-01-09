using Domain.Common;
using Domain.Common.Dtos.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IRoleInPermService
    {
        /// <summary>
        /// 根据角色id 获取菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<RoleInPermDto> GetPermsByRole(string roleId);

        /// <summary>
        /// 添加角色菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Result InsertRoleInPerm(RoleInPermDto dto);
    }
}
