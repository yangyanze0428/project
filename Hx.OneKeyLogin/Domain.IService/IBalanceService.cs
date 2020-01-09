using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface IBalanceService
    {
        List<BalanceDto> GetList(BalancePara para, out int total);
       Task < int> GetBalanceAsync(string userid);
        Task Rechange(BalanceDto dto, UserType roleId);
        /// <summary>
        /// 客户端在线充值
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task OnlineRechange(BalanceDto dto, UserType roleId);
    }
}
