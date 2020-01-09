using Domain.Common;
using Domain.Common.Dtos.Order;
using Domain.Common.Entities.Order;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Extensions;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
    public class ProdService : ServiceBase, IProdService
    {
        private readonly IProdRepository _prodRepository;
        private readonly IOrgRepository _orgRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IDataInOrgRepository _dataInOrgRepository;

        public ProdService(IProdRepository prodRepository, IOrgRepository orgRepository, IStaffRepository staffRepository, IDataInOrgRepository dataInOrgRepository)
        {
            _prodRepository = prodRepository;
            _orgRepository = orgRepository;
            _staffRepository = staffRepository;
            _dataInOrgRepository = dataInOrgRepository;
        }
        public List<ProdDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<ProdEntity>((int)roleId, orgId, DataType.Prod);
                return datas?.MapTo<List<ProdDto>>();
            });
            return rlt;
        }
        public List<ProdDto> GetList(ProdPara para, out int total)
        {
            try
            {
                var prods = GetList(para.RoleId, para.OrgId);
                var p = PredicateBuilder.Default<ProdDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                prods = prods.Where(p.Compile()).ToList();
                total = prods.Count;
                var rlt = prods.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
                return rlt;
            }
            catch (Exception e)
            {
                Logger.Error("select * from prod error：", e);
                total = 0;
                return new List<ProdDto>();
            }
        }
        public Result Add(ProdDto dto, UserType roleId)
        {
            try
            {
                var id = "";
                var entity = dto.MapTo<ProdEntity>();
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    id = _prodRepository.InsertAndGetId(entity);
                    var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = id,
                        DataType = (int)DataType.Prod,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);

                    return id.IsNotNullOrEmpty() && dataRlt;
                });
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("add prod error：", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }

        public Result Update(ProdDto dto)
        {
            try
            {
                var entity = dto.MapTo<ProdEntity>();
                var rlt = UnitOfWorkService.Execute(() => _prodRepository.Update(entity));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("update prod error：", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
        public Result Delete(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _prodRepository.Delete(id));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("delete prod error：", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
        public List<ProdDto> GetList(string userId)
        {
            try
            {

                var rlt = UnitOfWorkService.Execute(() => _prodRepository.GetList()
                    .Where(p => p.UserId == null || p.UserId.Equals(userId)).ToList()).OrderBy(a=>a.CreateDate);
                return rlt?.MapTo<List<ProdDto>>();
            }
            catch (Exception ex)
            {
                Logger.Error("getlist prod error：", ex);
                return new List<ProdDto>();
            }
        }
        public ProdDto Get(string id)
        {
            var rlt = UnitOfWorkService.Execute(() => _prodRepository.Get(id));
            return rlt.MapTo<ProdDto>();
        }
        public ProdDto GetNumber(int money)
        {
            var entity = UnitOfWorkService.Execute(() => _prodRepository.GetNumber(money));
            return entity?.MapTo<ProdDto>();
        }
    }
}
