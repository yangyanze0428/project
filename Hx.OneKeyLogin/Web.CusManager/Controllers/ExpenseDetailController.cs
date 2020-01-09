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
    public class ExpenseDetailController : ControllerBase
    {
        private IExpenseDetailService _expenseDetailService;

        public ExpenseDetailController(IExpenseDetailService expenseDetailService)
        {
            _expenseDetailService = expenseDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetList(ExpenseDetailPara para)
        {
            para.PageNumber = para.PageNumber - 1;
            para.UserId = LoginInfo.Id;
            return await Task.Run(() =>
            {
               var dtos = _expenseDetailService.GetList(para, out var count);
                var list = dtos.MapTo<List<ExpenseDetailModel>>();
                return Json(new { rows = list, total = count });
            });
        }
    }
}