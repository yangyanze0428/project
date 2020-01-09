using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.IService;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
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
        public async Task<JsonResult> GetList(BillingDetailsPara para)
        {
            para.PageNumber = para.PageNumber - 1;
            para.UserId = LoginInfo.Id;
            return await Task.Run(() =>
            {
                var dtos = _billingDetailsService.GetList(para, out var count);
                var list = dtos.MapTo<List<BillingDetailsModel>>();
                return Json(new { rows = list, total = count });
            });
        }
    }
}