using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Order;
using Domain.IService;
using Hx.Extensions;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    public class ProdController : ControllerBase
    {
        private readonly IProdService _prodService;
        private ICustomerService _customerService;

        public ProdController(IProdService prodService, ICustomerService customerService)
        {
            _prodService = prodService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetList(string userId, DateTime startTime, DateTime endTime, int page = 1, int rows = 20)
        {
            return await Task.Run(() =>
            {
                var para = new ProdPara
                {
                    UserId = userId,
                    StartTime = startTime,
                    EndTime = endTime,
                    PageNumber = page - 1,
                    PageSize = rows,
                    OrgId = LoginInfo.Org.Id,
                    RoleId = LoginInfo.Role
                };
                var rlt = _prodService.GetList(para, out var count);
                var data = rlt.MapTo<IEnumerable<ProdModel>>().ToList();
                return new JsonResult(new { total = count, rows = data });
            });
        }
        [Route("/prod/modi")]
        public IActionResult Modify(string opt, string id)
        {
            try
            {
                ViewBag.opt = opt;
                ViewBag.id = id;
                var model = new ProdDto();
                if (!id.Equals("0"))
                {
                    model = _prodService.Get(id);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error("prod错误", ex);
                return View(new ProdDto());
            }
        }
        [HttpPost]
        [Route("/prod/modi")]
        public async Task<Result> Modify(ProdDto dto, string opt)
        {
            try
            {
                var salesMan = "";
                if (dto.UserId.IsNotNullOrEmpty())
                {
                    salesMan=_customerService.Get(dto.UserId).SalesMan;
                }
                else
                {
                    salesMan =null;
                }
                var total = Math.Ceiling(dto.Money / dto.Price) + dto.Value;
                return await Task.Run(() =>
                {
                    dto.CreateDate = DateTime.Now;
                    dto.CreatorId = LoginInfo.Id;
                    dto.Total = decimal.ToInt32(total);//decimal.ToInt32(dto.Money / dto.Price) + dto.Value;
                    if (opt == "add")//新增
                    {
                        dto.Id = DateTime.Now.ToStringTimeStamp();// Guid.NewGuid().ToString();
                        dto.SalesMan =salesMan;//从客户表获取
                        return _prodService.Add(dto, LoginInfo.Role);
                    }
                    //修改
                    return _prodService.Update(dto);
                });
            }
            catch (Exception e)
            {
                Logger.Error("新增错误", e);
                return new Result { Status = false };
            }
        }
        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() => _prodService.Delete(id));
        }
    }
}