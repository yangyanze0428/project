using Hx.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.CusManager.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(ILogger logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.Error("Global Error", context.Exception);
            //var logger = _logger.CreateLogger(context.Exception.TargetSite.ReflectedType);
            //logger.LogError(new EventId(context.Exception.HResult),
            //context.Exception,
            //context.Exception.Message);

            var json = new ErrorResponse("未知错误,请重试");

            if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;
            
            context.Result = new ApplicationErrorResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //如果这里设为false，就表示告诉MVC框架，我没有处理这个错误,然后让它跳转到自己定义的错误页。
            //（设为true的话，就表示告诉MVC框架，异常我已经处理了。不需要在跳转到错误页了，也不会抛出黄页了）
            context.ExceptionHandled = false;
        }

        public class ApplicationErrorResult : ObjectResult
        {
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        public class ErrorResponse
        {
            public ErrorResponse(string msg)
            {
                Message = msg;
            }
            public string Message { get; set; }
            public object DeveloperMessage { get; set; }
        }
    }
}
