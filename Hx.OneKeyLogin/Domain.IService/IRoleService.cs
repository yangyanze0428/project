using Domain.Common;
using Domain.Common.Dtos.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
   public interface IRoleService
    {
        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDto GetRole(string id);

        List<RoleDto> GetRoleList();
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Result InsertRole(RoleDto dto);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Result UpdateRole(RoleDto dto);

        /// <summary>
        /// 删除失败
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result DeleteRole(string id);
    }
}
