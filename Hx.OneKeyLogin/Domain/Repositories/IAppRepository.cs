using Domain.Common.Entities.Application;
using Hx.Domain.Repositories;

namespace Domain.Common.Repositories
{
    public interface IAppRepository : IRepository<AppEntity, string>
    {
    }
}
