using Dapper;
using Domain.Common.Entities.Perms;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.OracleRepositories
{
    public class RoleInPermRepository : DapperRepositoryBase<RoleInPermEntity, long>, IRoleInPermRepository
    {
        public RoleInPermRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        public IEnumerable<RoleInPermEntity> GetPermsByRole(string roleId)
        {
            var sql = @"select t.*, t.rowid from Login_RoleInPerm t where t.roleid = :rid";
            var rlt = Connection.Query<RoleInPermEntity>(sql, new { rid = roleId });
            return rlt;
        }
    }
}
