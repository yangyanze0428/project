using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Enums;
using Domain.Common.Infra;
using Domain.IService;
using Hx.Extensions;
using Hx.Logging;
using Web.Api.Models;

namespace Web.Api.helpers
{
    public class FoundationVerification : Valid<Parameter>, IFoundationVerification
    {
        private readonly IAppService _appService;
        private ICustomerService _customer;
        public FoundationVerification(ILoggerFactory factory, IAppService appService, ICustomerService customer) : base(factory)
        {
            _appService = appService;
            _customer = customer;
            Add(IsNullOrSpaceTimeStampValid());
            Add(ParameterValid());
        }
        private Func<Parameter, Task<ValidationResult>> IsNullOrSpaceTimeStampValid()
        {
            return Mm;
            async Task<ValidationResult> Mm(Parameter m)
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
                var appinfo = _appService.GetList();
                var ap = appinfo?.FirstOrDefault(app => !string.IsNullOrWhiteSpace(app.Id) && app.Id.Equals(m.Appkey));
                if (ap == null) return await Task.FromResult(new ValidationResult(TaskResultStatus.AppKeyError.ToString()));
                var md5 = Hx.Security.Md5Getter.Get($"{ap.Id}{m.TimeStamp}{ap.MessageSecret}{m.Nonce}");
                if (!m.Signature.ToLower().Equals(md5.ToLower())) return await Task.FromResult(new ValidationResult(TaskResultStatus.SignatureError.ToString()));
                return ValidationResult.Success;
            };

        }

        private bool VerifTimeStamp(string timeStamp)
        {
            var pastTime = TimeStamp.GetLocalDateTimeSeconds(timeStamp);
            var st = DateTime.Now - pastTime;
            return st.TotalSeconds < 120;
        }
    }
}
