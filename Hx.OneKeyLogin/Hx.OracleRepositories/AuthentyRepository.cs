using Domain.Common.Entities.MemberShip;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Hx.Domain.Uow;

namespace Hx.OracleRepositories
{
    public class AuthentyRepository : DapperRepositoryBase<AuthentyEntity, string>, IAuthentyRepository
    {
        public AuthentyRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
