using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Components;
using Hx.ObjectMapping;
using Hx.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class RechargeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetList(RechargePara rechargePara, int page = 1, int rows = 20)
        {
            var service = ObjectContainer.Resolve<ICustomerService>();
            var staff = ObjectContainer.Resolve<IStaffService>();
            return await Task.Run(() =>
            {
                rechargePara.PageNumber = page - 1; //当前页
                rechargePara.PageSize = rows; //每页显示条数
                rechargePara.OrgId = LoginInfo.Org.Id;
                rechargePara.RoleId = LoginInfo.Role;

                var appService = ObjectContainer.Resolve<IRechargeService>();
                var rlt = appService.GetList(rechargePara, out var count);
                var data = rlt.MapTo<IEnumerable<RechargeModel>>().ToList();
                //data.ForEach(m => m.Price = service.Get(m.UserId).Price);
                data.ForEach(m => m.SalesMan = staff.Get(m.SalesMan)?.Name);
                var res = new JsonResult(new { total = count, rows = data });
                return res;
            });
        }
        public IActionResult Modify(string id, string opt)
        {
            var obj = ObjectContainer.Resolve<IRechargeService>();
            ViewBag.opt = opt;
            ViewBag.id = id;
            var model = new RechargeDto();
            if (!id.Equals("0"))
            {
                model = obj.Get(id);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<Result> Modify(RechargeDto dto, string opt)
        {
            var redis = ObjectContainer.Resolve<RedisHelper>();
            var balance = ObjectContainer.Resolve<IBalanceService>();
            var obj = ObjectContainer.Resolve<IRechargeService>();
            var b = new BalanceDto
            {
                Bank = dto.Bank,
                RechargeMode = dto.RechargeMode,
                SalesMan = dto.SalesMan,
                UserId = dto.UserId,
                Value = dto.Value,
                Money = dto.Money,
                OperatorId = LoginInfo.Id,
                ModifyDate = DateTime.Now,
                Remark = dto.Remark,
                TradeResult = TradeResult.Success,
                TradeType = TradeType.Offline,
                IdenTity = IdenTity.Server
            };
            if (opt == "add")
            {
                await balance.Rechange(b, LoginInfo.Role);
                await redis.StringGetAsync(RedisKeyName.CreateUserAmountKey(dto.UserId));
                await redis.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(dto.UserId), dto.Value);
                return new Result { Status = true };
            }
            return await Task.Run(() => obj.Update(dto));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            var obj = ObjectContainer.Resolve<IRechargeService>();
            return await Task.Run(() => obj.Delete(id));
        }
    }
}