using Domain.Common;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IUserInRoleService
    {
        Result AddUserInRole(UserInRoleDto dto);
        IEnumerable<UserInRoleEntity> GetListByUserId(string userId);
        UserInRoleDto GetRoleId(string userId);
    }
}
