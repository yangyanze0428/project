using Domain.Common.Entities.Balance;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;
using System.Threading.Tasks;
using Dapper;
using Domain.Common.Dtos.Balance;
using Hx.Extensions;

namespace Hx.OracleRepositories
{
    public class ExpenseDetailRepository : DapperRepositoryBase<ExpenseDetailEntity, long>, IExpenseDetailRepository
    {
        public ExpenseDetailRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }

        public IEnumerable<ExpenseStatis> Statis(string userId)
        {
            var sql = $@"select  trunc(createdate) createdate,count(*) totalcount from login_expensedetail where userid='{userId}' group by trunc(createdate) order by trunc(createdate)";
            var rlt = Connection.Query<ExpenseStatis>(sql);
            return rlt;
        }
    }
}
