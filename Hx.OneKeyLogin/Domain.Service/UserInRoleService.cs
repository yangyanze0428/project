using Domain.Common;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.Components;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
    public class UserInRoleService : ServiceBase, IUserInRoleService
    {
        private readonly IUserInRoleRepository _userInRoleRepository;

        public UserInRoleService(IUserInRoleRepository userInRoleRepository)
        {
            _userInRoleRepository = userInRoleRepository;
        }

        public Result AddUserInRole(UserInRoleDto dto)
        {
            try
            {
                
                var userInRole = dto.MapTo<UserInRoleEntity>();
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var entity = _userInRoleRepository.GetList().FirstOrDefault(m => m.UserId.Equals(dto.UserId));
                    if (entity != null)
                    {
                        entity.RoleId = userInRole.RoleId;
                        entity.UserId = userInRole.UserId;
                        var urlt = _userInRoleRepository.Update(entity);
                        return urlt;
                    }
                    var rlt1 = _userInRoleRepository.InsertAndGetId(userInRole);
                    return rlt1 > 0;
                });
                return !rlt ? new Result(false, "授权失败") : new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error($" add UserInRole to exception:{ex}");
                return new Result(false, "内部服务器错误");
            }
        }

        public UserInRoleDto GetRoleId(string userId)
        {
            var entities = UnitOfWorkService.Execute(() => _userInRoleRepository.GetList().ToList());
            var dto = entities.FirstOrDefault(m => m.UserId.Equals(userId))?.MapTo<UserInRoleDto>();
            return dto;
        }

        /// <summary>
        /// 根据用户id 查询角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserInRoleEntity> GetListByUserId(string userId)
        {
            try
            {
                return UnitOfWorkService.Execute(() => _userInRoleRepository.GetListByUserId(userId));
            }
            catch (Exception ex)
            {
                Logger.Error($" select UserInRole to exception:{ex}");
                return null;
            }
        }
    }
}
