using System.Threading.Tasks;
using Dapper;
using Domain.Common.Entities.Balance;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System.Linq;

namespace Hx.OracleRepositories
{
    public class BalanceRepository : DapperRepositoryBase<BalanceEntity, long>, IBalanceRepository
    {
        public BalanceRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
            
        }
       
        public async Task ChangeBalance(BalanceEntity balance)
        {
            var sql = @"merge into login_balance b using(select :userid as userid from dual) b2 on(b.userid=b2.userid)
                               when matched then
                              update  set b.beforeValue =(select value from login_balance t1 where t1.userid = b.userid), VALUE= (select value from login_balance t2 where t2.userid=b.userid)+:value,b.modifydate=sysdate
                              when not matched then
                              insert(b.userid, b.beforevalue, b.value, b.modifydate,b.remark,b.OperatorId,b.salesman) values(:userid,0,:value, sysdate,:remark,:OperatorId,:salesman)";
    
               await   Connection.ExecuteAsync(sql,new { userid=balance.UserId,value=balance.Value,remark=balance.Remark, OperatorId =balance.OperatorId, salesman =balance.SalesMan});

            
        }

        public async Task<int> GetBalanceAsync(string userid)
        {
            string sql = "select Value from login_balance where userid=:userid";

              return (await  Connection.QueryAsync<int>(sql,new { userid=userid})).FirstOrDefault();
        }
    }
}
