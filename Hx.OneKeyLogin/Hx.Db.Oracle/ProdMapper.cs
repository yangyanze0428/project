using DapperExtensions.Mapper;
using Domain.Common.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
   public class ProdMapper : ClassMapper<ProdEntity>
    {
        public ProdMapper()
        {
            Table("Login_Prod");
            AutoMap();
        }
    }
}
