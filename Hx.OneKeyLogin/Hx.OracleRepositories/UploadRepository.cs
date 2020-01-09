using Domain.Common.Entities.Upload;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Hx.Domain.Uow;

namespace Hx.OracleRepositories
{
    public class UploadRepository : DapperRepositoryBase<UploadEntity, long>, IUploadRepository
    {
        public UploadRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
