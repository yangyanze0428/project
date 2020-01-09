using Domain.Common.Entities.MemberShip;
using Domain.Common.Entities.Perms;
using Domain.Common.Repositories;
using Hx.Components;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System.Collections.Generic;
using System.Linq;

namespace Hx.OracleRepositories
{
    public class DataInOrgRepository: DapperRepositoryBase<DataInOrgEntity, long>, IDataInOrgRepository
    {
        public DataInOrgRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }

        public bool Inserts(DataInOrgEntity entity)
        {
            try
            {
                var org = ObjectContainer.Resolve<IOrgRepository>().Get(entity.OrgId);
                var orgs = ObjectContainer.Resolve<IOrgRepository>().GetList().ToList();
                var hset = new HashSet<long>();
                GetParent(hset, orgs, org);
                var rlt = 0;
                foreach (var item in hset)
                {
                    entity.OrgId = (int)item;
                    var id = base.InsertAndGetId(entity);
                    if (id > 0) rlt++;
                }
                return hset.Count == rlt;
            }
            catch (System.Exception ex)
            {
                Logger.Fatal("Insert DataInOrg error", ex);
                return false;
            }
        }

        private void GetParent(HashSet<long> hset, List<OrgEntity> perms, OrgEntity perm)
        {
            if (hset.Add(perm.Id))
            {
                if (!perm.ParentId.Equals("0"))
                {
                    var parentPerm = perms.FirstOrDefault(p => p.Id.Equals(perm.ParentId));
                    if (parentPerm != null)
                    {
                        GetParent(hset, perms, parentPerm);
                    }
                }
            }
        }
    }
}
