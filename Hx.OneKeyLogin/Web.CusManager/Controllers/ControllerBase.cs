using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.IService;
using Hx.Components;
using Hx.Logging;
using Hx.Serializing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        private ILogger _logger;
        protected ILogger Logger
        {
            get
            {
                if (_logger != null) return _logger;
                _logger = ObjectContainer.Resolve<ILogger>();
                return _logger;
            }
        }

        protected string UserId
        {
            get { return User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value; }
        }

        private LoginUser _loginInfo;
        protected LoginUser LoginInfo
        {
            get
            {
                if (_loginInfo != null) return _loginInfo;
                //如果 HttpContext.User.Identity.IsAuthenticated 为 true，或者 HttpContext.User.Claims.Count() > 0 表示用户已经登录
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst(ClaimTypes.Sid).Value;//读取userId
                    var userInfo = ObjectContainer.Resolve<ICustomerService>().Get(userId);
                    var authInfo = ObjectContainer.Resolve<IAuthentyService>().Get(userId);
                    if (userInfo == null)
                    {
                        throw new Exception("invalid user");
                    }
                    var loginUser = new LoginUser() //TODO：应保存登录人的什么信息
                    {
                        Id = userInfo.Id,
                        CompanyName = authInfo.Name,
                        License=authInfo.Licence,
                        Name=userInfo.BusinessContact,
                    };
                    return loginUser;
                }

                _loginInfo = new LoginUser();
                return _loginInfo;
            }
        }

        /// <summary>
        /// cookie中间件，存用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        ///  <param name="isRemember">记住密码</param>
        protected async Task CookieAuthorization(string userId, bool isRemember)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Sid, userId),
                //new Claim("auxUserName", auxUserName),//需要存储到cookie中的信息
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new AuthenticationProperties()
            {
                IssuedUtc = DateTimeOffset.UtcNow,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, authProperties);
            if (isRemember)
            {
                authProperties.IsPersistent = true;//cookie数据持久化
#if DEBUG
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(3);//cookie过期时间
#else
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7);//cookie过期时间
#endif
            }
        }

        protected ISerializer Serializer
        {
            get
            {
                var serializer = ObjectContainer.Resolve<ISerializer>();
                return serializer;
            }
        }
    }
}
