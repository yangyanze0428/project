using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.IService;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    public class BillingDetailsController : ControllerBase
    {
        private readonly IBillingDetailsService _billingDetailsService;

        public BillingDetailsController(IBillingDetailsService billingDetailsService)
        {
            _billingDetailsService = billingDetailsService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetList(BillingDetailsPara para, int page = 0, int rows = 20)
        {

            return await Task.Run(() =>
            {
                para.PageNumber = page - 1; //当前页
                para.PageSize = rows; //每页显示条数
                para.OrgId = LoginInfo.Org.Id;
                para.RoleId = LoginInfo.Role;
                var rlt = _billingDetailsService.GetList(para, out var count);
                var data = rlt.MapTo<List<BillingDetailsModel>>();
                return new JsonResult(new { total = count, rows = data });
            });
        }
    }
}