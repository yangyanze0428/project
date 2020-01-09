using Domain.Common.Entities.Expenses;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Dtos.Balance;

namespace Domain.Common.Repositories
{
    public interface IBillingDetailsRepository : IRepository<BillingDetailsEntity, long>
    {
        IEnumerable<BillingDetailsStatis> Statis(string userId, DateTime startTime, DateTime endTime);
    }
}
