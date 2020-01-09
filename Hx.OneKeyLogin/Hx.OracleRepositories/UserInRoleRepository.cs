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
    public class UserInRoleRepository : DapperRepositoryBase<UserInRoleEntity, long>, IUserInRoleRepository
    {
        public UserInRoleRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        public IEnumerable<UserInRoleEntity> GetListByUserId(string userId)
        {
            var sql = "select * from Login_userInRole where userid=: userid";
            var rlt = Connection.Query<UserInRoleEntity>(sql, new { userid = userId });
            return rlt ?? null;
        }
    }
}
