using DapperExtensions.Mapper;
using Domain.Common.Entities.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
     public class PermsMapper:ClassMapper<PermsEntity>
    {
        public PermsMapper()
        {
            Table("Login_Perms");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
