using Domain.Common.Entities.Balance;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;

namespace Domain.Common.Repositories
{
   public  interface IExpenseDetailRepository : IRepository<ExpenseDetailEntity, long>
    {
        IEnumerable<ExpenseStatis> Statis(string userId);
    }
}
