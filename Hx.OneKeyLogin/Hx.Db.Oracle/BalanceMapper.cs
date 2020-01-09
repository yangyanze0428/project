using DapperExtensions.Mapper;
using Domain.Common.Entities.Balance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class BalanceMapper : ClassMapper<BalanceEntity>
    {
        public BalanceMapper()
        {
            Table("Login_Balance");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
