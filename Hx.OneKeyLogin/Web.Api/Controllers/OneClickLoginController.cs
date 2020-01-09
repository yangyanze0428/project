using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Logging;
using Hx.ObjectMapping;
using Hx.Serializing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.helpers;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneClickLoginController : ControllerBase
    {
        private readonly IValidParameter _valid;
        private readonly IFoundationVerification _foundationVerification;
        private readonly IRequestGetMobileService _requesGetMobile;
        private ILogger Logger { get; set; }
        private readonly IPreTestService _preTestService;
        private ISerializer _serializer { get; set; }
        private IAppService _appService { get; set; }
        private ICustomerService _customerService { get; set; }
        public OneClickLoginController(IAppService appservice, ISerializer serializer, ILoggerFactory factory, IRequestGetMobileService mobileService, IPreTestService preTest, ICustomerService customerService, IValidParameter valid, IFoundationVerification foundationVerification)
        {
            _customerService = customerService;
            _valid = valid;
            _foundationVerification = foundationVerification;
            _preTestService = preTest;
            _requesGetMobile = mobileService;
            _appService = appservice;
            _serializer = serializer;
            Logger = factory.Create();
        }
        [HttpPost]
        [Route("/send")]
        public async Task<Result> Post([FromBody] Parameter parameter)
        {
            //计费在验证方法中
            try
            {
                var validRlt = await _valid.Vliad(parameter);
                if (validRlt != ValidationResult.Success) return new Result(false, validRlt.ErrorMessage);
                var p = parameter.MapTo<Params>();
                var rlt = await _requesGetMobile.GetMobile(p);
                return new Result(rlt.success, rlt.errorCode, body: rlt.result);
            }
            catch (Exception ex)
            {
                Logger.Error($"method:{nameof(Post)}", ex);
                return new Result(false, TaskResultStatus.SystemError.ToString(), body: null);
            }
        }
        [HttpPost]
        [Route("/pre")]
        public async Task<Result> PreTest([FromBody] PreTestModel pre)
        {
            try
            {
                var dto = pre.MapTo<PreTestDto>();
                var rlt = await _preTestService.PreTest(dto);
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("预验失败", ex);
                return new Result { Status = false, Message = TaskResultStatus.SystemError.ToString() };
            }
        }
        [HttpPost]
        [Route("/v1/send")]
        public async Task<Result> GetMobileNumber([FromBody] Parameter parameter)
        {
            try
            {
                var validRlt = await _foundationVerification.Vliad(parameter);
                if (validRlt != ValidationResult.Success) return new Result(false, validRlt.ErrorMessage);
                var p = parameter.MapTo<Params>();
                var rlt = await _requesGetMobile.GetMobileNumber(p);
                return new Result(rlt.success, rlt.errorCode, body: rlt.result);
            }
            catch (Exception ex)
            {
                Logger.Error($"method:{nameof(GetMobileNumber)}", ex);
                return new Result { Status = false, Message = TaskResultStatus.SystemError.ToString() };
            }
        }
        [HttpPost]
        [Route("/pretest")]
        public async Task<Result> PreTest([FromBody]PreTestPatams testPatams)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var appinfo = _appService.GetAppCache(testPatams.AppKey);
                if (appinfo == null) return new Result { Status = false, Message = TaskResultStatus.AppKeyError.ToString() };
                var json = AESAlgorithm.Decrypt(testPatams.PreData, appinfo.AppSecret);
                //var p= _serializer.Deserialize<PreTestModel>(json);
                // var dto = p.MapTo<PreTestDto>();
                var dto = new PreTestDto();
                var pArr = json.Split("&", StringSplitOptions.RemoveEmptyEntries);
                dto.AppKey = pArr[0];
                dto.AppSecret = pArr[1];
                dto.MerchantNo = pArr[2];
                var rlt = await _preTestService.PreTest(dto);
                sw.Stop();
                Logger.Info($"pretest time:{sw.ElapsedMilliseconds}");
                return rlt;

            }
            catch (Exception ex)
            {
                Logger.Error("预验失败", ex);
                return new Result { Status = false };
            }
        }
        [HttpPost]
        [Route("/sync")]
        public async Task<bool> Sync([FromBody] SyncModel sync)
        {
            try
            {
                switch (sync.Method)
                {
                    case 11:
                    case 12: _appService.Set(_serializer.Deserialize<AppDto>(sync.Body)); return await Task.FromResult(true);
                    case 13: _appService.Remove(sync.Body); return true;
                    case 21:
                    case 22: SyncCus(sync); return true;
                    case 23: _customerService.Remove(sync.Body); return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"同步发生异常:{ex}");
                return false;
            }
        }

        private void SyncCus(SyncModel sync)
        {
            var des = _serializer.Deserialize<CustomerDto>(sync.Body);
            _customerService.Set(des);
        }
    }

}