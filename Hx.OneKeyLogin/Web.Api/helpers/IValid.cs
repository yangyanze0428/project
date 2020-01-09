using Domain.Common.Enums;
using Hx.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.helpers
{
    public interface IValid<T>
    {
        void Add(Func<T, Task<ValidationResult>> func);
        Task<ValidationResult> Vliad(T data);
    }
    public interface IValidParameter : IValid<Parameter>
    {
    }
    public interface IFoundationVerification : IValid<Parameter>
    {
    }

    public abstract class Valid<T> : IValid<T>
    {
        List<Func<T, Task<ValidationResult>>> funcs = new List<Func<T, Task<ValidationResult>>>();
        ILogger Logger { get; set; }
        public Valid(ILoggerFactory factory)
        {
            Logger = factory.Create();
        }
        public async Task<ValidationResult> Vliad(T data)
        {
            try
            {
                foreach (var item in funcs)
                {
                    var rlt = await item(data);
                    if (rlt != ValidationResult.Success) return rlt;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("验证发生错误", ex);
                return new ValidationResult(TaskResultStatus.SystemError.ToString());
            }

            return ValidationResult.Success;
        }
        public void Add(Func<T, Task<ValidationResult>> func)
        {
            funcs.Add(func);
        }
    }
}
