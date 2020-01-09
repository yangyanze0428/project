using Domain.Common;
using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IRechargeService
    {
        RechargeDto GetNumber(string orderNumber);
        RechargeDto Get(string id);
        List<RechargeDto> GetList();
        List<RechargeDto> GetList(RechargePara para, out int total);
        Result Add(RechargeDto dto, UserType roleId);
        Result Delete(string id);
        Result Update(RechargeDto dto);
    }
}
