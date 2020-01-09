using Domain.Common.Entities.Order;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
    public interface IProdRepository : IRepository<ProdEntity, string>
    {
        ProdEntity GetNumber(int money);
    }
}
