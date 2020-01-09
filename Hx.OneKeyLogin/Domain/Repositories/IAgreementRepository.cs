using Domain.Common.Entities.Agreement;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
    public interface IAgreementRepository:IRepository<AgreementEntity,string>
    {
    }
}
