using Domain.IService;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Dtos.Balance;
using Domain.Common.Repositories;
using Hx;
using Domain.Common.Entities.Balance;
using System.Linq;
using Hx.ObjectMapping;
using Hx.Extensions;
using Domain.Common;
using Domain.Common.Entities.Expenses;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;

namespace Domain.Service
{
    public class RechargeService : ServiceBase,IRechargeService
    {
        private readonly IRechangeRepository _repo;
        private IDataInOrgRepository _dataInOrgRepository;
        private IStaffRepository _staffRepository;
        private IOrgRepository _orgRepository;
        public RechargeService(IRechangeRepository repo, IDataInOrgRepository dataInOrgRepository, IStaffRepository staffRepository,IOrgRepository orgRepository)
        {
            _repo = repo;
            _dataInOrgRepository = dataInOrgRepository;
            _staffRepository = staffRepository;
            _orgRepository = orgRepository;
        }
        public RechargeDto Get(string id)
        {
            var entity = UnitOfWorkService.Execute(() => _repo.Get(Convert.ToInt32(id)));
            return entity?.MapTo<RechargeDto>();
        }
        public RechargeDto GetNumber(string orderNumber)
        {
            var entity = UnitOfWorkService.Execute(() => _repo.GetNumber(orderNumber));
            return entity?.MapTo<RechargeDto>();
        }
        public List<RechargeDto> GetList()
        {
            var orderList = UnitOfWorkService.Execute(()=>_repo.GetList().ToList());
            return orderList?.MapTo<List<RechargeDto>>();
        }
        public List<RechargeDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<RechargeEntity>((int)roleId, orgId, DataType.Recharge);
                return datas?.MapTo<List<RechargeDto>>();
            });
            return rlt;
        }

        public List<RechargeDto> GetList(RechargePara para, out int total)
        {
            try
            {
                var recharges = GetList(para.RoleId, para.OrgId);
                var p = PredicateBuilder.Default<RechargeDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.SalesMan.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.SalesMan.Equals(para.SalesMan));//业务员
                }
                if (para.RechargeMode!=0)
                {
                    p = p.And(m => m.RechargeMode==para.RechargeMode);//充费模式
                }
                if (para.Bank != 0)
                {
                    p = p.And(m => m.Bank == para.Bank);//银行
                }
                if (para.IdenTity != 0)
                {
                    p = p.And(m => m.IdenTity == para.IdenTity);//标识
                }
                if (para.TradeType != 0)
                {
                    p = p.And(m => m.TradeType == para.TradeType);//支付方式
                }
                if (para.TradeResult != 0)
                {
                    p = p.And(m => m.TradeResult == para.TradeResult);//交易结果
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                //var recharge = UnitOfWorkService.Execute(() =>
                //{
                //    var list = _repo.GetPaged(p, para.PageNumber, para.PageSize, out count, false, m => m.CreateDate);
                //    return list?.Select(c => c.MapTo<RechargeDto>()).ToList();
                //});
                //if (recharges == null)
                //{
                //    total = 0;
                //    return new List<RechargeDto>();
                //}
                recharges = recharges.Where(p.Compile()).ToList();
                total = recharges.Count;
                return recharges.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("select * from Recharge error", e);
                total = 0;
                return new List<RechargeDto>();
            }
        }
        public Result Add(RechargeDto dto, UserType roleId)
        {
            try
            {
                var entity = dto.MapTo<RechargeEntity>();
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var id= _repo.InsertAndGetId(entity);
                    var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = id.ToString(),
                        DataType = (int)DataType.Recharge,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);
                    if (id<=0&&!dataRlt)
                    {
                        return new Result {Status = false, Message = "数据库操作失败" };
                    }

                    return new Result {Status = true};
                });
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("add Recharge error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
        public Result Delete(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _repo.Delete(Convert.ToInt32(id)));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("delete Recharge error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
        public Result Update(RechargeDto dto)
        {
            try
            {
                var entity = dto.MapTo<RechargeEntity>();
                var rlt = UnitOfWorkService.Execute(() => _repo.Update(entity));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("update Recharge error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }
    }
}
