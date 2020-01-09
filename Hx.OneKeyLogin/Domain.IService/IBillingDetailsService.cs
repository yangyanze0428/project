using Domain.Common.Dtos.Balance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IBillingDetailsService
    {
        List<BillingDetailsDto> GetList(BillingDetailsPara para, out int total);
        IEnumerable<BillingDetailsStatis> Statis(string userId, DateTime startTime, DateTime endTime);
    }
}
