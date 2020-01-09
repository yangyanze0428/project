using Dapper;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hx.OracleRepositories
{
    public class StaffRepository : DapperRepositoryBase<StaffEntity, string>, IStaffRepository
    {
        public StaffRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }

        public StaffEntity GetOrgId(string salesMan)
        {
            var sql = @"select a.id,a.name,a.password,a.phone,a.usertype,a.status,a.createdate,a.remark,a.orgid from login_staff a where a.id=:id";
            var rlt = Connection.Query<StaffEntity>(sql, new { id = salesMan });
            if (rlt.Count() == 0) return new StaffEntity();
            return rlt.FirstOrDefault();
        }
    }
}
