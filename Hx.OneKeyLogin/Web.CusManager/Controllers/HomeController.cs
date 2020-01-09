using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Dtos.Perms;
using Domain.IService;
using Hx.Components;
using Hx.Extensions;
using Hx.Serializing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        private IHostingEnvironment _env;
        private ICustomerService _customerService;
        private IPermsService _permsService;
        private IBalanceService _balanceService;
        private IBillingDetailsService  _billingDetailsService;
        public HomeController(IHostingEnvironment env, ICustomerService customerService, IPermsService permsService, IBalanceService balanceService, IBillingDetailsService billingDetailsService)
        {
            _env = env;
            _customerService = customerService;
            _permsService = permsService;
            _balanceService = balanceService;
            _billingDetailsService = billingDetailsService;
        }
        public IActionResult Index()
        {
            return View(LoginInfo);
        }


        public IActionResult Info()
        {
            ViewBag.Balance = _balanceService.GetBalanceAsync(LoginInfo.Id).Result;
            ViewBag.Company = LoginInfo.CompanyName.IsNullOrEmpty() ? "暂无企业信息" : LoginInfo.CompanyName;
            
            return View();
        }

        //public DateTime endTime { get;set; } = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //public DateTime startTime { get; set; } =Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
        public IActionResult GetCharts(DateTime startTime,DateTime endTime)
        {
            var list = _billingDetailsService.Statis(LoginInfo.Id,startTime,endTime);
            list.ForEach(m => m.FormatDate = m.CreateDate.ToString("yyyy-MM-dd"));
            return Json(list);
        }

        #region error
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
            var filePath = $"{_env.WebRootPath}/errors/{(statusCode == 404 ? 404 : 500)}.html";
            return new PhysicalFileResult(filePath, new MediaTypeHeaderValue("text/html"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region menu
        [Route("/menu")]
        public async Task<List<MenuModel>> CreateMenu()
        {
            var perms = GetMenu(); //根据当前登录用户，动态加载左侧菜单
            var menus = new List<MenuModel>();
            var serializer = ObjectContainer.Resolve<ISerializer>();
            if (serializer != null && perms == null)
            {
                Logger.Info(serializer.Serialize(perms));
            }
            CreateMenu(menus, perms, "0");

            return await Task.FromResult(menus);

        }

        private void CreateMenu(List<MenuModel> menus, List<PermsDto> perms, string pid)
        {
            var children = perms.Where(m => m.ParentId.Equals(pid)).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = new MenuModel { Id = child.Id, Text = child.Caption, Icon = child.Icon };
                if (!child.Module.Equals("-"))
                {
                    model.Attributes.Add("url", child.Module);
                    //model.State = "open";
                }

                menus.Add(model);
                CreateMenu(model.Nodes, perms, child.Id);
            }
        }
        /// <summary>
        /// 获取菜单栏
        /// </summary>
        private List<PermsDto> GetMenu()
        {
            var customerPerms = _customerService.Get(LoginInfo.Id).PermsId;//主账号权限
            var menuList = _permsService.GetPermsListById(customerPerms).Where(m => m.IsEnable == 1).OrderBy(m => m.OrderBy).ToList();//根据用户权限，动态加载左侧菜单
            return menuList;
        }
        #endregion
    }
}
