using Dapper;
using Domain.Common.Entities.Perms;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hx.OracleRepositories
{
    public class PermsRepository : DapperRepositoryBase<PermsEntity, string>, IPermsRepository
    {
        public PermsRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        public List<PermsEntity> GetPerms(string userId)
        {
            string sql =
                @"select distinct e.id,e.caption,e.parentid,e.module,e.orderby,e.description,e.platformtype,e.isenable
                from login_staff a 
                join login_role c on c.id=a.usertype
                join login_roleinperm d on d.roleid=c.id
                join login_perms e on e.id=d.permid where a.id = :id";
            var result = Connection.Query<PermsEntity>(sql, new { id = userId });
            return result.ToList();
        }
    }
}
