using DapperExtensions.Mapper;
using Domain.Common.Entities.MemberShip;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class AuthentyMapper:ClassMapper<AuthentyEntity>
    {
        public AuthentyMapper()
        {
            Table("Login_Authenty");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
