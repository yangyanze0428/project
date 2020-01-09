using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.IService;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class RechargeController : ControllerBase
    {
        private readonly IRechargeService _rechargeService;

        public RechargeController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetList(RechargePara para)
        {
            para.PageNumber = para.PageNumber - 1;
            para.UserId = LoginInfo.Id;
            return await Task.Run(() =>
            {
                var dtos = _rechargeService.GetList(para, out var count);
                var list = dtos.MapTo<List<RechargeModel>>(); 
                return Json(new { rows = list, total = count });
            });
        }
    }
}