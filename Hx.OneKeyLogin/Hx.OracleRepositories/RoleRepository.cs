using Domain.Common.Entities.Perms;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.OracleRepositories
{
    public class RoleRepository : DapperRepositoryBase<RoleEntity, string>, IRoleRepository
    {
        public RoleRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
