using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.Common.Entities.Balance;
using Domain.Common.Entities.Expenses;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Extensions;
using Hx.ObjectMapping;

namespace Domain.Service
{
    public class BalanceService : ServiceBase, IBalanceService
    {
        private readonly IBalanceRepository _repo;
        private readonly IRechangeRepository _rechangeRpo;
        private IOrgRepository _orgRepository;
        private IStaffRepository _staffRepository;
        private IDataInOrgRepository _dataInOrgRepository;
        public BalanceService(IBalanceRepository repo, IRechangeRepository rechange, IOrgRepository orgRepository, IStaffRepository staffRepository, IDataInOrgRepository dataInOrgRepository)
        {
            _rechangeRpo = rechange;
            _orgRepository = orgRepository;
            _staffRepository = staffRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _repo = repo;
        }
        public List<BalanceDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetData<BalanceEntity>((int)roleId, orgId, DataType.Balance);
                return datas?.MapTo<List<BalanceDto>>();
            });
            return rlt;
        }
        public List<BalanceDto> GetList(BalancePara para, out int total)
        {
            try
            {
                var count = 0;
                var balance = GetList(para.RoleId, para.OrgId);
                var p = PredicateBuilder.Default<BalanceDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (!string.IsNullOrEmpty(para.SalesMan))
                {
                    p = p.And(m => m.SalesMan.Contains(para.SalesMan));
                }
                //var balance = UnitOfWorkService.Execute(() =>
                // {
                //     var list = _repo.GetPaged(p, para.PageNumber, para.PageSize, out count, false, m => m.ModifyDate);
                //     return list?.Select(c => c.MapTo<BalanceDto>()).ToList();
                // });
                //if (balance == null)
                //{
                //    total = 0;
                //    return new List<BalanceDto>();
                //}
                //total = count;
                //return balance;
               
                balance = balance.Where(p.Compile()).ToList();
                total = balance.Count;
                return balance.OrderByDescending(m => m.ModifyDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("select * from Balance error", e);
                total = 0;
                return new List<BalanceDto>();
            }
        }

        /// <summary>
        /// 充值1，扣费-1
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task Rechange(BalanceDto dto,UserType roleId)
        {
            try
            {
                UnitOfWorkService.ExecuteNonQuery(async () =>
                {
                    await balance(dto,roleId);
                    await recharge(dto,roleId);
                });
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error("充值错误", ex);
                await Task.CompletedTask;
            }
        }
        public async Task OnlineRechange(BalanceDto dto, UserType roleId)
        {
            try
            {
                UnitOfWorkService.ExecuteNonQuery(async () =>
                {
                    await balance(dto, roleId);
                    //await recharge(dto, roleId);
                });
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error("充值错误", ex);
                await Task.CompletedTask;
            }
        }

        public async Task balance(BalanceDto dto, UserType roleId)
        {
            var entity = dto.MapTo<BalanceEntity>();
            await _repo.ChangeBalance(entity);
            var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
            var dataInOrg = new DataInOrgEntity
            {
                DataId = dto.UserId,
                DataType = (int)DataType.Balance,
                OrgId = orgId,
                RoleId = ((int)roleId).ToString()
            };
            _dataInOrgRepository.Inserts(dataInOrg);
        }

        public async Task recharge(BalanceDto dto, UserType roleId)
        {
            var rechangeEntity = dto.MapTo<RechargeEntity>();
            var id= _rechangeRpo.InsertAndGetId(rechangeEntity);
            var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
            var dataInOrg = new DataInOrgEntity
            {
                DataId = id.ToString(),
                DataType = (int)DataType.Recharge,
                OrgId = orgId,
                RoleId = ((int)roleId).ToString()
            };
            _dataInOrgRepository.Inserts(dataInOrg);
        }
        public async Task<int> GetBalanceAsync(string userid)
        {
            var t = await UnitOfWorkService.Execute(async () =>
            {
                var b = await _repo.GetBalanceAsync(userid);
                return b;
            });
            return t;
        }
    }

}
