using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.IService;
using Hx.Components;
using Hx.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.CmsManager.Controllers
{
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Login")]
        [HttpPost]
        public async Task<Result> Index(string name, string pwd, string rememberMe)
        {
            return await Task.Run(async () =>
            {
                var staffService = ObjectContainer.Resolve<IStaffService>();
                var dto = new StaffDto { Id = name, Password = pwd };
                var result = staffService.Verify(dto);

                if (result.Status) //登录成功
                {
                    await CookieAuthorizations(name, rememberMe); //cookie身份验证
                    result.Message = "home";
                    return result;
                }

                return result; //登录失败
            });
        }

        /// <summary>
        /// cookie中间件，存用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public async Task CookieAuthorizations(string userId, string rememberMe)
        {
            //创建一个身份认证
            var claims = new[] {
                new Claim(ClaimTypes.Sid, userId)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(claimsIdentity);// ClaimsIdentity的持有者就是 ClaimsPrincipal,,一个ClaimsPrincipal可以持有多个ClaimsIdentity，就比如一个人既持有驾照，又持有护照.

            if (rememberMe.IsNotEmpty() && rememberMe.Equals("on"))
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties
                {
                    IssuedUtc = DateTimeOffset.UtcNow,
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    AllowRefresh = false
                });
            }
        }
        /// <summary>
        /// 该Action从Asp.Net Core中注销登录的用户
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Logout()
        {
            //return await Task.Run(() =>
            //{
            //    if (HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
            //        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //    }

            //    return RedirectToPage("Login");
            //});
            return await Task.Run(() =>
            {
                HttpContext.SignOutAsync();
                return true;
            });
        }
    }
}