using Domain.Common.Entities.MemberShip;
using Domain.Common.Enums;
using Hx.Domain.Repositories;
using System.Collections.Generic;

namespace Domain.Common.Repositories
{
    public interface IOrgRepository : IRepository<OrgEntity, long>
    {
        OrgEntity GetOrgByUser(string userId);
        List<TEntity> GetDatas<TEntity>(int roleId, int orgId, DataType app);
        List<TEntity> GetData<TEntity>(int roleId, int orgId, DataType type);
    }
}
