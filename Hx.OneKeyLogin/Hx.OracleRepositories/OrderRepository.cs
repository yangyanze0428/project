using Domain.Common.Entities.Order;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.OracleRepositories
{
    public class OrderRepository : DapperRepositoryBase<OrderEntity, string>, IOrderRepository
    {
        public OrderRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
