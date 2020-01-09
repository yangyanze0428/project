using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace Hx.Web
{
    //public class Class1
    //{
    //}

    //public class HxAuthoricationMiddleware:AuthenticationMiddleware<AuthenticationOptions>
    //{
    //    public HxAuthoricationMiddleware(RequestDelegate next, IAuthenticationSchemeProvider schemes) : base(next, schemes)
    //    {
    //    }

        
    //}

    //public class HxAuthenticationOptions:IPoints

    public static class TenantMiddlewareExtension
    {
        public static IApplicationBuilder UseTenant(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
