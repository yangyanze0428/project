using Castle.Core.Internal;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.Application;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.ICacheManager;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
    public class AuthentyService : ServiceBase, IAuthentyService
    {
        private readonly IAuthentyRepository _authentyRepository;
        private ICustomerRepository _customerRepository;
        private ICustomerCache _customerCache;
        private IStaffRepository _staffRepository;
        private IDataInOrgRepository _dataInOrgRepository;
        private IOrgRepository _orgRepository;
        public AuthentyService(IAuthentyRepository authentyRepository, ICustomerRepository customerRepository, ICustomerCache customerCache, IStaffRepository staffRepository, IDataInOrgRepository dataInOrgRepository, IOrgRepository orgRepository)
        {
            _authentyRepository = authentyRepository;
            _customerRepository = customerRepository;
            _customerCache = customerCache;
            _staffRepository = staffRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _orgRepository = orgRepository;
        }
        public List<AuthentyDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<AuthentyEntity>((int)roleId, orgId, DataType.Authenty);
                return datas?.MapTo<List<AuthentyDto>>();
            });
            return rlt;
        }
        public List<AuthentyDto> GetList(AuthentyPara para, out int total)
        {
            try
            {
                var authenticity = GetList(para.RoleId, para.OrgId);
                var p = PredicateBuilder.Default<AuthentyDto>();
                if (!string.IsNullOrEmpty(para.Id))//账号
                {
                    p = p.And(m => m.Id.Contains(para.Id));
                }
                if (!string.IsNullOrEmpty(para.Name))
                {
                    p = p.And(m => m.Name.Contains(para.Name));
                }
                if (!string.IsNullOrEmpty(para.SalesMan))
                {
                    p = p.And(m => m.SalesMan.Contains(para.SalesMan));
                }
                authenticity = authenticity.Where(p.Compile()).ToList();
                total = authenticity.Count;
                var rlt = authenticity.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("select * from authenty error", ex);
                total = 0;
                return new List<AuthentyDto>();
            }
        }
        public Result Add(AuthentyDto dto, UserType roleId)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var id = _authentyRepository.InsertAndGetId(dto.MapTo<AuthentyEntity>());
                    var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = id,
                        DataType = (int)DataType.Authenty,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);
                    if (id.IsNullOrEmpty() && !dataRlt) return new Result(false, "数据库写入失败");
                    return new Result { Status = true };
                });
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("添加实名认证发生错误", ex);
                return new Result(false, "实名认证报错");
            }
        }

        public AuthentyDto Get(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _authentyRepository.Get(id));
                if (rlt == null) return new AuthentyDto();
                return rlt.MapTo<AuthentyDto>();
            }
            catch (Exception ex)
            {
                Logger.Error("获取企业认证信息失败", ex);
                return new AuthentyDto();
            }
        }

        public Result Delete(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _authentyRepository.Delete(id));
                if (rlt) return new Result { Status = true };
                return new Result { Status = false, Message = "数据库操作失败" };
            }
            catch (Exception ex)
            {
                Logger.Error("delete authenty error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }

        public Result Update(AuthentyDto dto)
        {
            try
            {
                var entity = dto.MapTo<AuthentyEntity>();
                var rlt = UnitOfWorkService.Execute(() => _authentyRepository.Update(entity));
                if (rlt) return new Result { Status = true };
                return new Result { Status = false, Message = "数据库操作失败" };
            }
            catch (Exception ex)
            {
                Logger.Error("update authenty error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
    }
}
