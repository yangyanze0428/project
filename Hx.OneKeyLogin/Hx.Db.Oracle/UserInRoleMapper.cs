using DapperExtensions.Mapper;
using Domain.Common.Entities.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
   public  class UserInRoleMapper:ClassMapper<UserInRoleEntity>
    {
        public UserInRoleMapper()
        {
            Table("Login_UserInRole");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
