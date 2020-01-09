using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Dtos.Perms;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Components;
using Hx.Extensions;
using Hx.ObjectMapping;
using Hx.Redis;
using Hx.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private IAuthentyService _authentyService;
        public CustomerController(ICustomerService customerService, IAuthentyService authentyService)
        {
            _customerService = customerService;
            _authentyService = authentyService;
        }
        [Route("/cus")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("/cus/getlist")]
        public async Task<JsonResult> GetList(string name, int page = 0, int rows = 20)
        {
            try
            {
                var balance = ObjectContainer.Resolve<IBalanceService>();
                var staff = ObjectContainer.Resolve<IStaffService>();
                return await Task.Run(() =>
                {
                    var para = new CustomerPara
                    {
                        Id = name,
                        PageNumber = page - 1,
                        PageSize = rows,
                        OrgId = LoginInfo.Org.Id,
                        RoleId = LoginInfo.Role
                    };
                    
                    var rlt = _customerService.GetModels(para, out var count);
                    var data = rlt.MapTo<List<Models.CustomerModel>>();
                    data.ForEach(async m => m.Balance = await balance.GetBalanceAsync(m.Id));
                    data.ForEach( m => m.SalesMan = staff.Get(m.SalesMan)?.Name);
                    data.ForEach(m => m.Name = _authentyService.Get(m.Id)?.Name);
                    data.ForEach(m => m.Licence = _authentyService.Get(m.Id)?.Licence);
                    return new JsonResult(new { total = count, rows = data });
                });
            }
            catch (Exception e)
            {
                Logger.Error("customerController GetList Error", e);
                return new JsonResult(new { total = 0, rows = "" });
            }
        }
        [Route("/cus/modi")]
        public ActionResult Modify(string id, string opt)
        {
            ViewBag.opt = opt;
            ViewBag.staff = GetStaffList();
            var model = new CustomerDto();
            if (opt.Equals("update"))
            {
                model = _customerService.Get(id);
            }
            return View(model);
        }
        [HttpPost]
        [Route("/cus/modi")]
        public async Task<Result> Modify(CustomerDto dto, string opt)
        {
            return await Task.Run(() =>
            {
                var request = Request.Form;
                dto.PermsId = request["clientperms"].ToString()?.TrimEnd(',');
                dto.Operator = LoginInfo.Id;
                if (opt.Equals("add"))//新增
                {
                    dto.Status = AccountStatus.Normal;
                    dto.CreateDate = DateTime.Now;
                    dto.PassWord = Hx.Security.Md5Getter.Get(dto.PassWord);
                    dto.Creator = LoginInfo.Id;
                    dto.AuditResult = AuditResult.Pending;
                    return _customerService.Add(dto,LoginInfo.Role);
                }
                //修改
                var newPwd = request["NewPassWord"].ToString();
                if (newPwd.IsNotNullOrEmpty())
                {
                    dto.PassWord = request["NewPassWord"];
                    dto.PassWord = Hx.Security.Md5Getter.Get(dto.PassWord);
                }
                return _customerService.Update(dto);
            });
        }

        public IActionResult Audit()
        {
            return View();
        }

        [Route("/cus/delete")]
        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() =>
            {
                return _customerService.Delete(id);
            });
        }
        [Route("/cus/pass")]
        public async Task<Result> Pass(string id)
        {
            return await Task.Run(() => _customerService.Pass(id));
        }
        /// <summary>
        /// 账户唯一验证/账号是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/cust")]
        public async Task<bool> UserExist(string id)
        {
            if (RegexHelper.IsMobile(id) || RegexHelper.IsEmail(id))
            {
                var user = _customerService.Get(id);
                return await Task.FromResult(user.Id.IsNullOrEmpty());
            }
            return false;
        }
        /// <summary>
        /// /账号不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/cust/exist")]
        public bool Exist(string id)
        {
            var user = _customerService.Get(id);
            return user.Id != null;
        }

        /// <summary>
        /// 客户功能选项
        /// </summary>
        /// <returns></returns>
        [Route("/cus/tree")]
        public async Task<JsonResult> CreateMenu()
        {
            var permsService = ObjectContainer.Resolve<IPermsService>();
            var permsDtos = permsService.GetPermsList(1).Where(m => m.IsEnable == 1).ToList();
            var menus = new List<MenuModel>();
            CreateMenu(menus, permsDtos, "0");
            return await Task.FromResult(Json(menus));
        }

        private void CreateMenu(List<MenuModel> menus, List<PermsDto> perms, string pid)
        {
            var children = perms.Where(m => m.ParentId.Equals(pid)).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = new MenuModel { Id = child.Id, Text = child.Caption, State = "open" };
                menus.Add(model);
                CreateMenu(model.Children, perms, child.Id);
            }
        }
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns></returns>
        public SelectList GetStaffList()
        {
            var staffService = ObjectContainer.Resolve<IStaffService>();
            var list = new List<SelectListItem>();
            var rlt = staffService.GetList()
                    .Where(m => m.Status == AccountStatus.Normal).ToList();
            foreach (var item in rlt)
            {
                var text = item.Name;
                var id = item.Id;
                list.Add(new SelectListItem { Text = text, Value = id });
            }
            return new SelectList(list, "Value", "Text");
        }
        [HttpPost]
        [Route("/verify")]
        public bool Verify(string id)
        {
            return RegexHelper.IsEmail(id);
        }
        #region 充值
        [Route("/cus/re")]
        public async Task<ActionResult> Rechange(string id)
        {
            try
            {
                ViewBag.id = id;
                var balance = ObjectContainer.Resolve<IBalanceService>();
                var dbBalance = await balance.GetBalanceAsync(id);
                ViewBag.DbBalance = dbBalance; //balance.GetBalanceAsync(id, DetectionCategory.NumberDetection);
                var value = await ObjectContainer.Resolve<RedisHelper>().StringGetAsync(RedisKeyName.CreateUserAmountKey(id));
                ViewBag.RedisBalance = value.IsNullOrWhiteSpace() ? 0 : Convert.ToInt32(value);
                return View();
            }
            catch (Exception e)
            {
                Logger.Error($"db and redis error:", e);
                return null;
            }
        }
        [Route("/cus/re")]
        [HttpPost]
        public async Task<Result> Rechange(string id, string money, string remark)
        {

            try
            {
                if (!int.TryParse(money, out var result)) return new Result(false, "充值额度输入错误");
                var balance = ObjectContainer.Resolve<IBalanceService>();
                var dto=_customerService.Get(id);
                var val = result / dto.Price;//条数
                var s=Math.Ceiling(val);//Math.Ceiling：向上取整，只要有小数都加1
                await balance.Rechange(new Domain.Common.Dtos.Balance.BalanceDto { UserId = id, Value =(int)s,Money=result,OperatorId = LoginInfo.Id, ModifyDate = DateTime.Now, Remark = remark },LoginInfo.Role);
                await ObjectContainer.Resolve<RedisHelper>().StringGetAsync(RedisKeyName.CreateUserAmountKey(id));
                await ObjectContainer.Resolve<RedisHelper>().StringIncrementAsync(RedisKeyName.CreateUserAmountKey(id), (int)s);
                return new Result(true, "充值成功");
            }
            catch (Exception ex)
            {
                Logger.Error("充值错误:", ex);
                return new Result(false,"系统错误");
            }
        }
        #endregion

        [HttpPost]
        [Route("/staff/query")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> Query(string q)
        {
            var staffService = ObjectContainer.Resolve<IStaffService>();
            var staff =staffService.GetList().Where(c => c.Status == AccountStatus.Normal).ToDictionary(c => c.Id, c => c.Name.IsNullOrEmpty() ? $"{c.Id}" : $"{c.Id} | {c.Name}");
            var rlt = staff.Where(item =>
                item.Key.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (item.Value.IsNullOrEmpty() ? false : item.Value.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0));
            return await Task.FromResult(rlt);
        }
        [HttpPost]
        [Route("/cus/query")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> CusQuery(string q)
        {
          var staff = _customerService.GetList().Where(c => c.Status == AccountStatus.Normal)
              .ToDictionary(c => c.Id, c => c.Id.IsNotNullOrEmpty() ? $"{c.Id}" : $"{c.Id} | {c.Id}");
            var rlt = staff.Where(item =>
                item.Key.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (item.Value.IsNullOrEmpty() ? false : item.Value.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0));
            return await Task.FromResult(rlt);
        }
    }
}