using Domain.Common.Entities.Order;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hx.Domain.Uow;
using Dapper;

namespace Hx.OracleRepositories
{
    public class ProdRepository : DapperRepositoryBase<ProdEntity, string>, IProdRepository
    {
        public ProdRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        public ProdEntity GetNumber(int money)
        {
            var sql = @"select * from LOGIN_prod b where b.money=:money";
            var rlt = Connection.Query<ProdEntity>(sql, new { money = money });
            return rlt.FirstOrDefault();
        }
    }
}
