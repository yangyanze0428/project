using Domain.IService;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Dtos.Perms;
using Domain.Common.Repositories;
using Hx.ObjectMapping;
using System.Linq;
using Domain.Common;
using Domain.Common.Entities.Perms;
using Hx.Extensions;

namespace Domain.Service
{
    public class RoleService : ServiceBase, IRoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public RoleDto GetRole(string id)
        {
            return UnitOfWorkService.Execute(() => _repo.Get(id)).MapTo<RoleDto>();
        }

        public List<RoleDto> GetRoleList()
        {
            return UnitOfWorkService.Execute(() =>
             {
                 var list = _repo.GetList().ToList();
                 return list.MapTo<List<RoleDto>>();
             });
        }

        public Result InsertRole(RoleDto dto)
        {
            try
            {
                var model = dto.MapTo<RoleEntity>();
                var id = UnitOfWorkService.Execute(() => _repo.InsertAndGetId(model));
                if (!id.IsNullOrWhiteSpace()) return new Result(false, "");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error($"add role to exception :{ex}");
                return new Result(false,"添加失败");
            }
        }

        public Result UpdateRole(RoleDto dto)
        {
            try
            {
                var model = dto.MapTo<RoleEntity>();
                var rlt = UnitOfWorkService.Execute(() => _repo.Update(model));
                if (!rlt) return new Result(false, "");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error($"update role to exception:{ex}");
                return new Result(false, "修改失败");
            }
        }

        public Result DeleteRole(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _repo.Delete(id));
                return !rlt ? new Result(false, "") : new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error($" delete role to exception:{ex}");
                return new Result(false, "删除失败");
            }
        }
    }
}
