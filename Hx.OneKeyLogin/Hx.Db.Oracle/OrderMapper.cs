using DapperExtensions.Mapper;
using Domain.Common.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class OrderMapper: ClassMapper<OrderEntity>
    {
        public OrderMapper()
        {
            Table("Login_Order");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
