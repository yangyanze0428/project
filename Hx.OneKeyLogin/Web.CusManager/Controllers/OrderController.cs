using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Order;
using Domain.IService;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetList(OrderPara para, int page = 1, int rows = 20)
        {
            para.PageNumber = page - 1; //当前页
            para.PageSize = rows; //每页显示条数
            para.UserId = LoginInfo.Id;
            return await Task.Run(() =>
            {
                var count = 0;
                var dtos = _orderService.GetList(para, out count);
                var list = dtos.MapTo<List<OrderModel>>();
                return Json(new { rows = list, total = count });
            });
        }
    }
}