using DapperExtensions.Mapper;
using Domain.Common.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
   public class BillingDetailsMapper : ClassMapper<BillingDetailsEntity>
    {
        public BillingDetailsMapper()
        {
            Table("Login_BillingDetails");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
