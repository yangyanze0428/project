using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Order;
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
    public class OrderController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetList(string id, string userId, string name, OrderType orderType, DateTime startTime, DateTime endTime, int page = 1, int rows = 20)
        {
            return await Task.Run(() =>
            {
                var para = new OrderPara
                {
                    Id = id,
                    UserId = userId,
                    Name = name,
                    OrderType = orderType,
                    StartTime = startTime,
                    EndTime = endTime,
                    PageNumber = page - 1,
                    PageSize = rows
                };
                var staffService = ObjectContainer.Resolve<IOrderService>();
                var rlt = staffService.GetList(para, out var count);
                var data = rlt.MapTo<IEnumerable<OrderModel>>().ToList();
                return new JsonResult(new { total = count, rows = data });
            });
        }
        /// <summary>
        /// 增改页面
        /// </summary>
        /// <param name="opt">add/update</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Modify(string opt = "", string id = "")
        {
            ViewBag.opt = opt;
            ViewBag.id = id;
            var model = new OrderDto();
            if (!id.Equals("0"))
            {
                var orderService = ObjectContainer.Resolve<IOrderService>();
                model = orderService.Get(id);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<Result> Modify(OrderDto dto, string opt)
        {
            var orderService = ObjectContainer.Resolve<IOrderService>();
            if (opt != "add")
            {
                return await Task.Run(() => orderService.Update(dto));
            }
            dto.CreateDate = DateTime.Now;
            //todo: dto.Id 页面上不增加 订单id存什么格式比如yyyyMMdd
            return await Task.Run(() => orderService.Add(dto));
        }
        [HttpPost]
        public async Task<Result> Delete(string id)
        {
            var orderService = ObjectContainer.Resolve<IOrderService>();
            return await Task.Run(() =>
            {
                var del = orderService.Delete(id);
                return del;
            });
        }
    }
}