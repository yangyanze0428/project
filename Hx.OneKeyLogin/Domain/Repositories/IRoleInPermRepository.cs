using Domain.Common.Entities.Perms;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
    public interface IRoleInPermRepository:IRepository<RoleInPermEntity,long>
    {
        IEnumerable<RoleInPermEntity> GetPermsByRole(string roleId);
    }
}
