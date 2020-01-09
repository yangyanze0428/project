using Domain.IService;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Dtos.Perms;
using Domain.Common.Repositories;
using System.Linq;
using Hx.ObjectMapping;
using Domain.Common.Entities.Perms;
using Domain.Common;

namespace Domain.Service
{
    public class RoleInPermService : ServiceBase, IRoleInPermService
    {
        private readonly IRoleInPermRepository _repo;

        public RoleInPermService(IRoleInPermRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<RoleInPermDto> GetPermsByRole(string roleId)
        {
            var rlt = UnitOfWorkService.Execute(() => _repo.GetPermsByRole(roleId));
            return rlt.Select(m => m.MapTo<RoleInPermDto>());
        }
        public Result InsertRoleInPerm(RoleInPermDto dto)
        {
            try
            {
                var model = dto.MapTo<RoleInPermEntity>();
                var id = UnitOfWorkService.Execute(() => _repo.InsertAndGetId(model));
                if (id <= 0) return new Result(false, "添加失败");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error($"add roleInPerm to exception:{ex}");
                return new Result(false, "");
            }
        }
    }
}
