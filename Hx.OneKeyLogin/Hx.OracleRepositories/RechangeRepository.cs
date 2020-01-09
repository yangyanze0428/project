using Domain.Common.Entities.Balance;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;
using Dapper;
using System.Linq;

namespace Hx.OracleRepositories
{
    public class RechangeRepository : DapperRepositoryBase<RechargeEntity, long>, IRechangeRepository
    {
        public RechangeRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        public RechargeEntity GetNumber(string orderNumber)
        {
            var sql = @"select * from LOGIN_RECHARGE b where b.ordernumber=:ordernumber";
            var rlt = Connection.Query<RechargeEntity>(sql, new { ordernumber = orderNumber });
            return rlt.FirstOrDefault();
        }
    }
}
