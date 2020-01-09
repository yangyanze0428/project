using DapperExtensions.Mapper;
using Domain.Common.Entities.MemberShip;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class StaffMapper : ClassMapper<StaffEntity>
    {
        public StaffMapper()
        {
            Table("Login_Staff");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
