using Domain.Common.Entities.Balance;
using Hx.Domain.Repositories;
using System.Threading.Tasks;

namespace Domain.Common.Repositories
{
    public interface IBalanceRepository : IRepository<BalanceEntity, long>
    {
        /// <summary>
        /// 扣费或者充值接口 
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        Task ChangeBalance(BalanceEntity balance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<int> GetBalanceAsync(string userid);
    }
}
