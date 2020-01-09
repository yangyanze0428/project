using DapperExtensions.Mapper;
using Domain.Common.Entities.Balance;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;

namespace Hx.Db.Oracle
{
   public class ExpenseDetailMapper : ClassMapper<ExpenseDetailEntity>
    {
        public ExpenseDetailMapper()
        {
            Table("Login_ExpenseDetail");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
