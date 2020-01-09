using DapperExtensions.Mapper;
using Domain.Common.Entities.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class RoleMapper:ClassMapper<RoleEntity>
    {
        public RoleMapper()
        {

            Table("Login_Role");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();

        }
    }
}
