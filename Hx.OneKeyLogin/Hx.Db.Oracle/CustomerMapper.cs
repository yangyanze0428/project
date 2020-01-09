using DapperExtensions.Mapper;
using Domain.Common.Entities.MemberShip;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class CustomerMapper : ClassMapper<CustomerEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerMapper()
        {
            Table("Login_Customer");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
