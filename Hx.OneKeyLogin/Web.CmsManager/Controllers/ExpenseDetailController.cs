using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using Domain.IService;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class ExpenseDetailController : ControllerBase
    {
        private readonly IExpenseDetailService _expenseDetailService;

        public ExpenseDetailController(IExpenseDetailService expenseDetailService)
        {
            _expenseDetailService = expenseDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetList(ExpenseDetailPara para,int page = 0, int rows = 20)
        {
           
            return await Task.Run(() =>
            {
                para.PageNumber = page - 1; //当前页
                para.PageSize = rows; //每页显示条数
                para.OrgId = LoginInfo.Org.Id;
                para.RoleId = LoginInfo.Role;
                var rlt =_expenseDetailService.GetList(para, out var count);
                var data=rlt.MapTo<List<ExpenseDetailModel>>();
                return new JsonResult(new { total = count, rows = data });
            });
        }
    }
}