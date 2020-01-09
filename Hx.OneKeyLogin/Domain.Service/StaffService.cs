using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Components;
using Hx.Extensions;
using Hx.ObjectMapping;

namespace Domain.Service
{
    public class StaffService : ServiceBase, IStaffService
    {
        private readonly IStaffRepository _repo;
        private readonly IUserInRoleRepository _userInRoleRepository;
        private readonly IDataInOrgRepository _dataInOrgRepository;
        private readonly IOrgRepository _orgRepository;
        public StaffService(IStaffRepository repo, IUserInRoleRepository userInRoleRepository, IDataInOrgRepository dataInOrgRepository,IOrgRepository orgRepository)
        {
            _repo = repo;
            _userInRoleRepository = userInRoleRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _orgRepository = orgRepository;
        }

        public StaffDto Get(string userId)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _repo.Get(userId));
                return rlt?.MapTo<StaffDto>();
            }
            catch (Exception ex)
            {
                Logger.Error("获取员工信息时发生异常", ex);
                return new StaffDto();
            }
        }

        public List<StaffDto> GetList()
        {
            return UnitOfWorkService.Execute(() => _repo.GetList().ToList().MapTo<List<StaffDto>>());
        }

        public List<StaffDto> GetStaff(UserType roleId, int orgId)
        {
          return UnitOfWorkService.Execute(() =>GetList(roleId,orgId));
        }

        public List<StaffDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<StaffEntity>((int)roleId, orgId, DataType.Staff);
                return datas?.MapTo<List<StaffDto>>();
            });
            return rlt;
        }

        public List<StaffDto> GetList(StaffPara para, out int count)
        {
            // var staffs = GetList().Where(a => a.Status < AccountStatus.Obsolete);
            var p = PredicateBuilder.False<StaffDto>();

            if (!string.IsNullOrEmpty(para.Id))
            {
                p = p.Or(s => s.Id.Contains(para.Id));
            }

            if (!string.IsNullOrEmpty(para.RealName))
            {
                p = p.Or(m => m.Name.Contains(para.RealName));
            }

            if (p.Parameters[0].Name.IsNotNullOrEmpty())
            {
                p = PredicateBuilder.True<StaffDto>();
            }

            var staffs = GetList(para.RoleId,para.OrgId).Where(p.Compile());
            count = staffs.Count();
            var rlt = staffs.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            return rlt;
        }

        /// <summary>
        /// 增加员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <inheritdoc cref="" />
        public Result<string> Add(StaffDto dto, int orgId, UserType roleId)
        {
            try
            {
                var staff = dto.MapTo<StaffEntity>();
                var userInRole = new UserInRoleEntity
                {
                    RoleId = ((int)dto.UserType).ToString(),
                    UserId = dto.Id
                };
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var sta = _repo.Get(dto.Id);
                    if (sta != null)
                    {
                        return new Result<string> { Status = false, Message = "该账号已存在" };
                    }
                    var rlt1 = _repo.InsertAndGetId(staff);
                    var rlt2 = _userInRoleRepository.InsertAndGetId(userInRole);
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = rlt1,
                        DataType = (int)DataType.Staff,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);//数据部门权限
                    var executeRlt = rlt1.IsNotEmpty() && rlt2 > 0 && dataRlt;
                    if (executeRlt) return new Result<string> { Status = true };

                    return new Result<string> { Status = false, Message = "数据库写入失败" };
                });

                if (!rlt.Status)
                {
                    Logger.Error($"新增员工失败：{dto.Id}");
                    return rlt;
                }
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error($" add staff to exception:{ex}");
                return new Result<string> { Status = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <inheritdoc cref="" />
        public Result<string> Update(StaffDto dto)
        {
            try
            {
                var staff = dto.MapTo<StaffEntity>();
                var rlt = UnitOfWorkService.Execute(() =>
                    {
                        var rlt1 = ObjectContainer.Resolve<IStaffRepository>().Update(staff);
                        if (rlt1) return new Result<string> { Status = true };
                        return new Result<string> { Status = false, Message = "数据库写入失败" };
                    }
                );
                if (!rlt.Status)
                {
                    Logger.Error($"修改员工失败：{dto.Id}");
                    return rlt;
                }
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error($"update staff to exception:{ex}");
                return new Result<string> { Status = false, Message = ex.Message };
            }
        }

        public Result Delete(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() =>
               {
                   var entity = _repo.Get(id);
                   if (entity == null)
                   {
                       return new Result(false, "id不存在", "");
                   }
                   entity.Status = (int)AccountStatus.Obsolete;
                   _repo.Update(entity);
                   return new Result(true, "", "");
               });
                return rlt;
            }
            catch (Exception ex)
            {

                Logger.Error("禁用员工发生异常", ex);
                return new Result(false, "禁用失败", "");
            }
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result Pass(string id)
        {
            return UnitOfWorkService.Execute(() =>
            {
                var entity = _repo.Get(id);
                if (entity == null) return new Result { Status = false };
                entity.Status = 1;
                _repo.Update(entity);
                return new Result { Status = true };
            });
        }
        public Result Verify(StaffDto dto)
        {
            try
            {
                var list = UnitOfWorkService.Execute(() => _repo.Get(dto.Id));
                if (list == null) return new Result { Status = false, Message = "账户名或密码错误" };
                if (list.Status == 8)
                {
                    return new Result { Status = false, Message = "该员工禁止登录" };
                }
                dto.Password = Hx.Security.Md5Getter.Get(dto.Password);
                return list.Password.Equals(dto.Password, StringComparison.OrdinalIgnoreCase) ? new Result(true, "") : new Result { Status = false, Message = "账户名或密码错误" };
            }
            catch (Exception ex)
            {
                Logger.Error("login error", ex);
                return new Result(false, "账户名或密码错误");
            }
        }
    }
}
