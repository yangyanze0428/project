using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.CusManager.Controllers
{
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private ICustomerService _customerService;
        public LoginController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<Result> Index(string userName, string password, bool isRemember)
        {
            try
            {
                var result = _customerService.Verify(userName,password);
                return await Task.Run(async () =>
                {
                    if (result.Status)//登录成功
                    {
                        await CookieAuthorization(((CustomerDto)result.Body).Id, isRemember);
                        result.Message = "home";
                        return result;
                    }
                    return result; //登录失败
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Login.Verify failure", ex);
                return new Result(false, "服务器产生错误");
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns>登录页面</returns>
        public async Task<bool> Logout()
        {
            try
            {
                return await Task.Run(() =>
                {
                    HttpContext.SignOutAsync();
                    return true;
                });
            }
            catch (Exception ex)
            {
                Logger.Error("logout error", ex);
                return false;
            }
            
        }
        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JsonResult Exist(string username)
        {
            var m = _customerService.Get(username);
            return Json(m != null ? new { valid = true } : new { valid = false });
        }
    }
}