using DapperExtensions.Mapper;
using Domain.Common.Entities.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
   public class RoleInPermMapper : ClassMapper<RoleInPermEntity>
    {
        public RoleInPermMapper()
        {
            Table("login_roleinperm");
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
