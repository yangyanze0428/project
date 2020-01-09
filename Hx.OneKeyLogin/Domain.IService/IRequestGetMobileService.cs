using Domain.Common.Dtos.App;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface IRequestGetMobileService
    {
        Task<RespResult> GetMobile(Params @params);
        Task<RespResult> GetMobileNumber(Params @params);
    }
}
