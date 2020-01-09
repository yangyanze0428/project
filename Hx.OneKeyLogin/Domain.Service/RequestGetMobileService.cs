using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Entities.Balance;
using Domain.Common.Entities.Expenses;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Infra;
using Domain.Common.Repositories;
using Domain.IService;
using Domain.Service.MediatHandler;
using Hx;
using Hx.Components;
using Hx.Configurations.Application;
using Hx.HttpClientFactory;
using Hx.Redis;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class RequestGetMobileService : ServiceBase, IRequestGetMobileService
    {
        private readonly IHxHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly IGuidGenerator _guid;
        private IMediator _mediator;
        private readonly IBalanceRepository _balanceRepository;
        private readonly RedisHelper _redisHelper;
        private readonly IExpenseDetailRepository _expenseDetailRepository;
        private readonly IDataInOrgRepository _dataInOrgRepository;
        private readonly IStaffRepository _staffRepository;
        public RequestGetMobileService(Func<string, IGuidGenerator> guidGenerator, IHxHttpClientFactory httpClientFactory, IConfiguration config, IGuidGenerator guid, IMediator mediator, IBalanceRepository balanceRepository, RedisHelper redisHelper, IExpenseDetailRepository expenseDetailRepository, IDataInOrgRepository dataInOrgRepository, IStaffRepository staffRepository)
        {
            _guid = guidGenerator("database"); ;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _mediator = mediator;
            _balanceRepository = balanceRepository;
            _redisHelper = redisHelper;
            _expenseDetailRepository = expenseDetailRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _staffRepository = staffRepository;
        }
        /// <summary>
        /// "phoneNum": "17621826129"
        //"operatorType": "CT"
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<RespResult> GetMobile(Params @params)
        {
            try
            {
                var nonce = _guid.Create().ToString("N");
                var dateTime = DateTimeOffset.UtcNow;
                var timeStamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var appInfo = ObjectContainer.Resolve<IAppService>().GetAppCache(@params.AppKey);
                var dataContent = AESAlgorithm.Decrypt(@params.DataContent, appInfo.MessageSecret);
                @params.DataContent = AESAlgorithm.Encrypt(dataContent, appInfo.MsgSecret);

                var url = _config.GetValue("OneKeyLogin:Url", "https://feiyan.xinyan-ai.com/ocl/oclOrder/v1/getMobile");
                var respResult = await Send(@params, nonce, timeStamp, appInfo, url);

                if (!respResult.success) return respResult;
                var cmd = new SendCompleteCommand();
                CreateCompleteCommand(@params, nonce, dateTime, appInfo, respResult, cmd);

                //计费
                await Balance(appInfo);
                
                //redis
                await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(appInfo.UserId), -1);
                var entity = new ExpenseDetailEntity();
                CreateEntity(cmd, entity);
                
                //交易明细写数据库
                await ExpenseDetail(appInfo, entity);
                //await _mediator.Publish(cmd);
                return respResult;

            }
            catch (Exception ex)
            {
                Logger.Error("GetMobile:", ex);
                Logger.Error($"json:{Serializer.Serialize(@params)}");
                return new RespResult { errorCode = TaskResultStatus.SystemError.ToString(), errorMsg = "系统错误", result = null, success = false };
            }
        }
        public async Task<RespResult> GetMobileNumber(Params @params)
        {
            try
            {
                var nonce = _guid.Create().ToString("N");
                var dateTime = DateTimeOffset.UtcNow;
                var timeStamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var appInfo = ObjectContainer.Resolve<IAppService>().GetAppCache(@params.AppKey);
                var dataContent = AESAlgorithm.Decrypt(@params.DataContent, appInfo.MessageSecret);
                @params.DataContent = AESAlgorithm.Encrypt(dataContent, appInfo.MsgSecret);

                var url = _config.GetValue("OneKeyLogin:Url", "https://feiyan.xinyan-ai.com/ocl/oclOrder/v1/getMobile");
                var respResult = await Send(@params, nonce, timeStamp, appInfo, url);

                if (!respResult.success) return respResult;
                var cmd = new SendCompleteCommand();
                CreateCompleteCommand(@params, nonce, dateTime, appInfo, respResult, cmd);

                var entity = new ExpenseDetailEntity();
                CreateEntity(cmd, entity);

                await ExpenseDetail(appInfo, entity);

                return respResult;

            }
            catch (Exception ex)
            {
                Logger.Error("GetMobileNumber:", ex);
                Logger.Error($"json:{Serializer.Serialize(@params)}");
                return new RespResult { errorCode = TaskResultStatus.SystemError.ToString(), errorMsg = "系统错误", result = null, success = false };
            }
        }

        private async Task ExpenseDetail(AppDto appInfo, ExpenseDetailEntity entity)
        {
            await UnitOfWorkService.Execute(async () =>
            {
                var id = await _expenseDetailRepository.InsertAndGetIdAsync(entity);
                var orgId = _staffRepository.GetOrgId(appInfo.SalesMan).OrgId;
                var dataInOrg = new DataInOrgEntity
                {
                    DataId = id.ToString(),
                    DataType = (int)DataType.ExpenseDetail,
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
                await _balanceRepository.ChangeBalance(new BalanceEntity
                { UserId = appInfo.UserId, Value = -1, Remark = "计费程序", SalesMan = appInfo.SalesMan });
                var orgId = _staffRepository.GetOrgId(appInfo.SalesMan).OrgId;
                var dataInOrg = new DataInOrgEntity
                {
                    DataId = appInfo.UserId,//客户id
                    DataType = (int)DataType.Balance,
                    OrgId = orgId,
                    RoleId = ((int)UserType.Admin).ToString()
                };
                _dataInOrgRepository.Inserts(dataInOrg);
            });
        }

        private static void CreateEntity(SendCompleteCommand notification, ExpenseDetailEntity entity)
        {
            entity.Appkey = notification.AppKey;
            entity.Description = notification.Description;
            entity.Mobile = notification.Mobile;
            entity.OperatorType = OperatorCon(notification.OperatorType);
            entity.Remark = "";
            entity.Success = notification.Result;
            entity.UserId = notification.UserId;
            entity.Value = -1;
            entity.CreateDate = DateTime.Now;
            entity.Description = "";
            entity.SalesMan = notification.SalesMan;
        }

        public static int OperatorCon(string op)
        {
            switch (op)
            {
                case "CT": return 3;
                case "CU": return 2;
                case "CM": return 1;
                default: break;
            }
            return 0;
        }
        private static void CreateCompleteCommand(Params @params, string nonce, DateTimeOffset dateTime, AppDto appInfo, RespResult respResult, SendCompleteCommand cmd)
        {
            cmd.AppKey = @params.AppKey;
            cmd.UserId = appInfo.UserId;
            cmd.Mobile = respResult.result == null ? "" : respResult.result[ResponseDicKey.Mobile];
            cmd.ReceiveTime = dateTime.LocalDateTime;
            cmd.UserTimeStamp = TimeStamp.GetLocalDateTimeSeconds(@params.TimeStamp);
            cmd.UserNonce = @params.Nonce;
            cmd.Result = Convert.ToInt32(respResult.success);
            cmd.SendSuccessTime = DateTime.Now;
            cmd.Nonce = nonce;
            cmd.CreateDate = DateTime.Now;
            cmd.OperatorType = respResult.result == null ? "" : respResult.result[ResponseDicKey.OperatorType];
            cmd.Description = respResult.errorMsg;
            cmd.SalesMan = appInfo.SalesMan;
            cmd.Signature = appInfo.Signature;
        }

        private async Task<RespResult> Send(Params @params, string nonce, string timeStamp, AppDto appInfo, string url)
        {

            var rqstParams = new RequestParams();
            CreateRequestParams(@params, nonce, timeStamp, appInfo, rqstParams);
            try
            {
                var resp = await _httpClientFactory.CreateHttpClient().SendAsync(url, Serializer.Serialize(rqstParams));
                if (resp.StatusCode != HttpStatusCode.OK) return await Task.FromResult(new RespResult { success = false, errorCode = TaskResultStatus.ChannelValidFail.ToString() });
                var mobile = await resp.Content.ReadAsStringAsync();
                return Serializer.Deserialize<RespResult>(mobile);
            }
            catch (Exception ex)
            {
                Logger.Error("调用getMobile接口错误", ex);
                return await Task.FromResult(new RespResult { success = false, errorCode = TaskResultStatus.SystemError.ToString() });
            }

        }

        private void CreateRequestParams(Params @params, string nonce, string timeStamp, AppDto appInfo, RequestParams rqstParams)
        {
            rqstParams.appKey = @params.AppKey;
            rqstParams.dataContent = @params.DataContent;
            rqstParams.signature = CreateSignature(appInfo, nonce, timeStamp, @params.DataContent);
            rqstParams.timestamp = timeStamp;
            rqstParams.nonce = nonce;
        }

        private string CreateSignature(AppDto app, string nonce, string timestamp, string dataContent)
        {

            var signature = $"{app.Id}&{nonce}&{timestamp}&{dataContent}&{app.MsgSecret}";
            return Hx.Security.Md5Getter.Get(signature).ToLower();
        }
    }
}
