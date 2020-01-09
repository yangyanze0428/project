using Domain.Common.Entities.Perms;
using Hx.Domain.Repositories;

namespace Domain.Common.Repositories
{
    public interface IDataInOrgRepository: IRepository<DataInOrgEntity, long>
    {
        bool Inserts(DataInOrgEntity entity);
    }
}
