using DapperExtensions.Mapper;
using Domain.Common.Entities.Balance;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;

namespace Hx.Db.Oracle
{
    public class RechangeMapper : ClassMapper<RechargeEntity>
    {
        public RechangeMapper()
        {
            Table("login_recharge");
            Map(x=>x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
