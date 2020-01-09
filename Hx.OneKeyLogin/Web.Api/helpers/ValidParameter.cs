using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Infra;
using Domain.IService;
using Hx.Extensions;
using Hx.Logging;
using Hx.Redis;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.helpers
{
    public class ValidParameter : Valid<Parameter>, IValidParameter
    {
        private readonly IAppService _appService;
        private readonly RedisHelper _redisHelper;
        private IMediator _mediator;
        private readonly ICustomerService _customer;
        private ILogger logger;
        public ValidParameter(IAppService appService, RedisHelper redis, ILoggerFactory factory, IMediator mediator, ICustomerService customer) : base(factory)
        {
            _customer = customer;
            _mediator = mediator;
            logger = factory.Create();
            _redisHelper = redis;
            _appService = appService;
            Add(IsNullOrSpaceTimeStampValid());
            Add(ParameterValid());
            Add(BalanceValid());
        }

        private Func<Parameter, Task<ValidationResult>> IsNullOrSpaceTimeStampValid()
        {
            return MM;
            async Task<ValidationResult> MM(Parameter m)
            {
                if (m.Appkey.IsNullOrWhiteSpace() || m.TimeStamp.IsNullOrWhiteSpace() || m.Nonce.IsNullOrWhiteSpace() ||
                m.Signature.IsNullOrWhiteSpace() || m.DataContent.IsNullOrWhiteSpace())
                    return await Task.FromResult(new ValidationResult(TaskResultStatus.AppConfigNotError.ToString()));
                if (!VerifTimeStamp(m.TimeStamp)) return await Task.FromResult(new ValidationResult(TaskResultStatus.TimeStampError.ToString()));
                return await Task.FromResult(ValidationResult.Success);
            }
        }

        private Func<Parameter, Task<ValidationResult>> ParameterValid()
        {

            return async m =>
               {
                   //appkey 查找对应的密码信息
                   var appinfo = _appService.GetList();
                   var ap = appinfo?.FirstOrDefault(app => !string.IsNullOrWhiteSpace(app.Id) && app.Id.Equals(m.Appkey));
                   if (ap == null) return await Task.FromResult(new ValidationResult(TaskResultStatus.AppKeyError.ToString()));
                   var md5 = Hx.Security.Md5Getter.Get($"{ap.Id}{m.TimeStamp}{ap.MessageSecret}{m.Nonce}");
                   if (!m.Signature.ToLower().Equals(md5.ToLower())) return await Task.FromResult(new ValidationResult(TaskResultStatus.SignatureError.ToString()));
                   return ValidationResult.Success;
               };

        }

        //验证余额
        private Func<Parameter, Task<ValidationResult>> BalanceValid()
        {
            return async m =>
            {

                var appinfo = _appService.GetAppCache(m.Appkey);
                var ba = await _redisHelper.StringGetAsync(RedisKeyName.CreateUserAmountKey(appinfo?.UserId));
                if (ba == null)
                {
                    ba = "0";
                }
                var balance = Convert.ToInt32(ba);
                var customer = _customer.GetCache(appinfo.UserId);
                if (customer.PayMode == PayMode.PostPay) return ValidationResult.Success;
                if (balance <= 0) return new ValidationResult(TaskResultStatus.Arrears.ToString());
                return ValidationResult.Success;
            };
        }

        ////计费
        //private Func<Parameter, Task<ValidationResult>> Charging() 
        //{
        //    return async m =>
        //    {
        //        var appinfo = _appService.GetApp(m.Appkey);
        //        await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(appinfo.UserId), -1);
        //        await _mediator.Send(m);
        //        return ValidationResult.Success;
        //    };
        //} 

        private bool VerifTimeStamp(string timetamp)
        {
            var pastTime = TimeStamp.GetLocalDateTimeSeconds(timetamp);
            var st = DateTime.Now - pastTime;
            return st.TotalSeconds < 120;
        }
    }


}
