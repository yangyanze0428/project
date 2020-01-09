using Domain.Common.Entities.MemberShip;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
     public interface ICustomerRepository : IRepository<CustomerEntity,string>
    {
        IEnumerable<CustomerEntity> GetTest();
    }
}
