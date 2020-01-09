using Domain.Common.Enums;
using Hx.Extensions;
using System;

namespace Domain.Common
{
    public class Result
    {
        public Result() { }
        public Result(bool status, string message, object body = null)
        {
            Status = status;
            Message = message;
            Body = body;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }

    public class Result<T>
    {
        public Result() { }

        public Result(bool status, string message, T body = default)
        {
            Status = status;
            Message = message;
            Body = body;
        }

        public bool Status { get; set; }
        public string Message { get; set; }

        public T Body { get; set; }
    }

    /// <summary>
    /// 交易结果
    /// </summary>
    public class FinTransResult : Result<double>
    {
        public FinTransResult(JobResultStatus status,double balance=0d)
        {
            ResultStatus = status;
            Status=JobResultStatus.OK==status;
            Message = status.GetDescription();
            Body = balance;
        }

        public JobResultStatus ResultStatus { get; set; }
    }
}
