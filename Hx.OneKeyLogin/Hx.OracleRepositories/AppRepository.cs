using Domain.Common.Entities.Application;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;

namespace Hx.OracleRepositories
{
    public class AppRepository : DapperRepositoryBase<AppEntity, string>, IAppRepository
    {
        public AppRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
    }
}
