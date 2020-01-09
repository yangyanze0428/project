using Domain.Common.Entities.Order;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
    public interface IOrderRepository:IRepository<OrderEntity,string>
    {
    }
}
