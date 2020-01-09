using Domain.Common.Entities.Balance;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;

namespace Domain.Common.Repositories
{
    public interface IRechangeRepository:IRepository<RechargeEntity,long>
    {
        RechargeEntity GetNumber(string orderNumber);
    }
}
