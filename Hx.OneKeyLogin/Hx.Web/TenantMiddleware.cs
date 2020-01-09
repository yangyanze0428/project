using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hx.Configurations.Startup;
using Hx.Extensions;
using Hx.Modules;
using Hx.Utilities;
using Microsoft.AspNetCore.Http;

namespace Hx.Web
{
    public class TenantMiddleware
    {
        private static readonly string[] UrlStart =  {"/login", "/reg", "/rst", "/forget" };
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!IfProcess(context))
            {
                await _next.Invoke(context);
                return;
            }

            var hostString = context.Request.Host.Host;
            var hostType = GetHostType(hostString);
            var tenantKey = "";

            if (hostType == HostType.DomainName)
            {
                tenantKey = hostString.Split(".")[0];
            }else if (context.Request.Query.ContainsKey("tid"))
            {
                tenantKey = context.Request.Query["tid"];
            }
            else if (context.Request.HasFormContentType && context.Request.Form.ContainsKey("tid"))
            {
                tenantKey = context.Request.Form["tid"];
            }

            //var tenantId = tenantKey.Length > 0 ? TenantHepler.GetIdByKey(tenantKey.ToLower()) : "";

            //if (tenantId.IsNullOrEmpty())
            //{
            //    //context.Response.Redirect("~/Error");

            //    context.Response.ContentType = "text/plain;charset=utf-8";

            //    await context.Response.WriteAsync("缺少必要的Tenant信息");
            //    return;
            //}
            
            //context.Items.Add("tenant",tenantId);

            await _next.Invoke(context);
        }

        public bool IfProcess(HttpContext context)
        {
            var isGet = context.Request.Method.Equals("get", StringComparison.OrdinalIgnoreCase);
            return isGet && UrlStart.Any(u => context.Request.Path.StartsWithSegments(u, StringComparison.OrdinalIgnoreCase));
        }

        HostType GetHostType(string host)
        {
            if (RegexHelper.IsDomainName(host))
            {
                return HostType.DomainName;
            }

            if (RegexHelper.IsIP(host))
            {
                return HostType.Ip;
            }

            if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                return HostType.Local;
            }

            return HostType.UnKnown;
        }
    }

    enum HostType
    {
        UnKnown,
        Local,
        Ip,
        DomainName
    }
}