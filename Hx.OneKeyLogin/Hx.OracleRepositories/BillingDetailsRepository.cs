using Domain.Common.Entities.Expenses;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Hx.Domain.Uow;
using Domain.Common.Dtos.Balance;
using Dapper;

namespace Hx.OracleRepositories
{
    public class BillingDetailsRepository : DapperRepositoryBase<BillingDetailsEntity, long>, IBillingDetailsRepository
    {
        public BillingDetailsRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }

        public IEnumerable<BillingDetailsStatis> BillingDetailsStatis(string userId, DateTime startTime, DateTime endTime)
        {
            var sql =
                $@"select  trunc(createdate) createdate,count(*) totalcount from login_billingdetails 
                where userid = :userid and createdate>= :startTime and
                createdate < :endTime group by trunc(createdate) order by trunc(createdate) ";
            return Connection.Query<BillingDetailsStatis>(sql,new {userId=userId,startTime=startTime,endTime=endTime});
        }
        public IEnumerable<BillingDetailsStatis> Statis(string userId,DateTime startTime,DateTime endTime)
        {
            //var sql = $@"select  trunc(createdate) createdate,count(*) totalcount from login_billingdetails where userid='{userId}' group by trunc(createdate) order by trunc(createdate)";
            //var rlt = Connection.Query<BillingDetailsStatis>(sql);
            //return rlt;
            var sql =
                $@"select  trunc(createdate) createdate,count(*) totalcount from login_billingdetails 
                where userid = :userid and createdate>= :startTime and
                createdate < :endTime group by trunc(createdate) order by trunc(createdate) ";
            return Connection.Query<BillingDetailsStatis>(sql, new { userId = userId, startTime = startTime, endTime = endTime });
            //select  trunc(createdate) createdate,count(*) totalcount from login_billingdetails 
            // where userid = '17744477229' and createdate>= to_date('2019-12-2', 'yyyy-MM-dd') and
            //createdate < to_date('2019-12-3', 'yyyy-MM-dd') group by trunc(createdate) order by trunc(createdate) desc
        }
    }
}
