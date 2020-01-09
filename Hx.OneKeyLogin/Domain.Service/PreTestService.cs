using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Entities.Balance;
using Domain.Common.Entities.Expenses;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Infra;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.ICacheManager;
using Hx.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class PreTestService : ServiceBase, IPreTestService
    {
        private readonly IAppService _appService;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IDataInOrgRepository _dataInOrgRepository;
        private readonly RedisHelper _redisHelper;
        private readonly IBillingDetailsRepository _billingDetailsRepository;
        private readonly ICustomerCache _customerCache;
        public PreTestService(IAppService appService, IBalanceRepository balanceRepository, IStaffRepository staffRepository, IDataInOrgRepository dataInOrgRepository, RedisHelper redisHelper, IBillingDetailsRepository billingDetailsRepository, ICustomerCache customerCache)
        {
            _appService = appService;
            _balanceRepository = balanceRepository;
            _staffRepository = staffRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _redisHelper = redisHelper;
            _billingDetailsRepository = billingDetailsRepository;
            _customerCache = customerCache;
        }
        public async Task<Result> PreTest(PreTestDto dto)
        {
            try
            {
                var appInfo = _appService.GetAppCache(dto.AppKey);
                if (appInfo == null) return new Result { Status = false, Message = TaskResultStatus.AppKeyError.ToString() };
                if (!VerifTimeStamp(dto.TimeStamp)) return await Task.FromResult(new Result { Status = false, Message = TaskResultStatus.TimeStampError.ToString() });
                var md5 = Hx.Security.Md5Getter.Get($"{appInfo.Id}{appInfo.AppSecret}{dto.TimeStamp}{appInfo.UserId}");
                if (!dto.Signature.ToLower().Equals(md5.ToLower())) return new Result { Status = false, Message = TaskResultStatus.PreSignatureError.ToString() };
                
                //验证余额
                var ver = await VerifiedBalance(appInfo);
                if (!ver.Status) return ver;

                var dataContent = AESAlgorithm.Decrypt(dto.DataContent, appInfo.AppSecret);
                if (string.IsNullOrWhiteSpace(dataContent)) return new Result {Status = false, Message = TaskResultStatus.TokenError.ToString()};
                //计费
                await Balance(appInfo);

                //redis
                await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(appInfo.UserId), -1);

                //计费明细写数据库
                await BillingDetails(appInfo, dataContent);
                return new Result { Status = true,Message=TaskResultStatus.OK.ToString() };
            }
            catch (Exception ex)
            {
                Logger.Error($"PreTest error:{ex},dto:{Serializer.Serialize(dto)}");
                return new Result { Status = false, Message = TaskResultStatus.SystemError.ToString() };
            }
        }

        private async Task<Result> VerifiedBalance(AppDto appInfo)
        {
            var bal = await UnitOfWorkService.Execute(async () =>
               {
                   var customer = _customerCache.Get(appInfo.UserId);
                   if (customer == null) return new Result { Status = false, Message = TaskResultStatus.Other.ToString() };
                   if (customer.PayMode == PayMode.PostPay) return new Result { Status = true };
                   var ba = await _redisHelper.StringGetAsync(RedisKeyName.CreateUserAmountKey(appInfo.UserId));
                   if (ba == null)
                   {
                       ba = "0";
                   }
                   var balance = Convert.ToInt32(ba);
                   if (balance <= 0) return new Result { Status = false, Message = TaskResultStatus.Arrears.ToString() };
                   return new Result { Status = true };
               });
            return bal;
        }

        private async Task BillingDetails(AppDto appInfo, string dataContent)
        {
            _ = await UnitOfWorkService.Execute(async () =>
               {
                   var entity = new BillingDetailsEntity
                   {
                       AppKey = appInfo.Id,
                       Token = dataContent,
                       UserId = appInfo.UserId,
                       Value = -1,
                       CreateDate = DateTime.Now,
                       ChargeType = 1,
                       SalesMan = appInfo.SalesMan,
                       Remark = ""
                   };
                   var id = await _billingDetailsRepository.InsertAndGetIdAsync(entity);
                   var orgId = _staffRepository.GetOrgId(appInfo.SalesMan).OrgId;
                   var dataInOrg = new DataInOrgEntity
                   {
                       DataId = id.ToString(),
                       DataType = (int)DataType.BillingDetails,
                       OrgId = orgId,
                       RoleId = ((int)UserType.Admin).ToString()
                   };
                   var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);
                   if (id <= 0 && !dataRlt)
                   {
                       return new Result { Status = false, Message = "数据库操作失败" };
                   }
                   return new Result { Status = true };
               });
        }

        private async Task Balance(AppDto appInfo)
        {
            await UnitOfWorkService.Execute(async () =>
            {
                var entity = new BalanceEntity
                {
                    UserId = appInfo.UserId,
                    Value = -1,
                    Remark = "计费程序",
                    SalesMan = appInfo.SalesMan
                };
                await _balanceRepository.ChangeBalance(entity);
                var orgId = _staffRepository.GetOrgId(appInfo.SalesMan).OrgId;
                var dataInOrg = new DataInOrgEntity
                {
                    DataId = appInfo.UserId,
                    DataType = (int)DataType.Balance,
                    OrgId = orgId,
                    RoleId = ((int)UserType.Admin).ToString()
                };
                _dataInOrgRepository.Inserts(dataInOrg);
            });
        }

        private bool VerifTimeStamp(string timeStamp)
        {
            var pastTime = TimeStamp.GetLocalDateTimeSeconds(timeStamp);
            var st = DateTime.Now - pastTime;
            return st.TotalSeconds < 120;
        }
    }
}
