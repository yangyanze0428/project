using Domain.Common.Entities.Agreement;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.OracleRepositories
{
    public class AgreementRepository : DapperRepositoryBase<AgreementEntity, string>, IAgreementRepository
    {
        public AgreementRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
