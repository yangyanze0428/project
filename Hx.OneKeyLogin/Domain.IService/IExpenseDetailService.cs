using Domain.Common.Dtos.Balance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IExpenseDetailService
    {
        List<ExpenseDetailDto> GetList(ExpenseDetailPara para, out int total);
        IEnumerable<ExpenseStatis> Statis(string userId);
    }
}
