using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Components;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class BalanceController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetList(BalancePara para, int page = 1, int rows = 20)
        {
            return await Task.Run(() =>
            {
                para.PageNumber = page - 1;
                para.PageSize = rows;
                para.RoleId = LoginInfo.Role;
                para.OrgId = LoginInfo.Org.Id;
                var staffService = ObjectContainer.Resolve<IBalanceService>();
                var rlt = staffService.GetList(para, out var count);
                var data = rlt.MapTo<IEnumerable<BalanceModel>>().ToList();
                return new JsonResult(new { total = count, rows = data });
            });
        }
    }
}