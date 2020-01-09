using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Components;
using Hx.Extensions;
using Hx.ObjectMapping;
using Hx.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class StaffController : ControllerBase
    {
        private IStaffService _staffService;
        private IOrgService _orgService;
        private IRoleService _roleService;
        public StaffController(IStaffService staffService,IOrgService orgService, IRoleService roleService)
        {
            _staffService = staffService;
            _orgService = orgService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="staffname">用户名</param>
        /// <param name="page">页数</param>
        /// <param name="rows">条数</param>
        /// <returns></returns>
        public async Task<JsonResult> GetList(string username = "", string staffname = "", int page = 0, int rows = 20)
        {
            return await Task.Run(() =>
            {
                var para = new StaffPara
                {
                    Id = username,
                    RealName = staffname,
                    PageNumber = page - 1,
                    PageSize = rows,
                    OrgId = LoginInfo.Org.Id,
                    RoleId = LoginInfo.Role
                };
                var rlt = _staffService.GetList(para, out var count).OrderBy(m => m.Name);
                var data = rlt.MapTo<IEnumerable<StaffModel>>().ToList();
                data.ForEach(m => m.OrgIdDesc = _orgService.Get(m.OrgId)?.Name);
                var res = new JsonResult(new { total = count, rows = data });
                return res;
            });
        }
        [Route("UserType")]
        public JsonResult GetUserType()
        {
            var list = new List<IdText>
            {
                new IdText {Id = "0", Text = "请选择", selected = true}
            };
            var role = _roleService.GetRoleList();
            foreach (var item in role)
            {
                var litem = new IdText { Text = item.RoleName, Id = item.Id };
                list.Add(litem);
            }
            return Json(list);
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
           // ViewBag.ulists = EnumExt.GetSelectList();
            var model = new StaffDto();
            if (!id.Equals("0"))
            {
                model = Get(id);
            }
            return View(model);
        }
        /// <summary>
        /// 获取单个数据 
        /// </summary>
        /// <param name="id">账户名/id</param>
        /// <returns></returns>
        public StaffDto Get(string id)
        {
            return _staffService.Get(id);
        }
        [HttpPost]
        public async Task<Result<string>> Modify(StaffDto dto, string staffopt)
        {
            var request = Request.Form;
            if (staffopt == "add")
            {
                dto.CreateDate = DateTime.Now;
                dto.Status = AccountStatus.Normal;
                dto.Password = Hx.Security.Md5Getter.Get(dto.Password);
                return await Task.Run(() => _staffService.Add(dto, dto.OrgId, LoginInfo.Role));
            }
            var newPwd = request["NewPassWord"].ToString();
            if (newPwd.IsNotNullOrEmpty())
            {
                dto.Password = request["NewPassWord"];
                dto.Password = Hx.Security.Md5Getter.Get(dto.Password);
            }
            return await Task.Run(() => _staffService.Update(dto));
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id"></param>                                                                                              
        ///<returns></returns>
        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() =>
            {
                var rlt = _staffService.Delete(id);
                return rlt;
            });
        }
        /// <summary>
        ///启用
        /// </summary>
        /// <param name="id"></param>                                                                                              
        ///<returns></returns>
        public async Task<Result> Pass(string id)
        {
            return await Task.Run(() =>
            {
                var rlt = _staffService.Pass(id);
                return rlt;
            });
        }
        /// <summary>
        /// 账户唯一验证
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/stav")]
        public async Task<bool> UserExist(string id)
        {
            return await Task.Run(() =>
            {
                if (RegexHelper.IsMobile(id) || RegexHelper.IsEmail(id))
                {
                    var user = _staffService.Get(id);
                    return user == null;
                }
                return false;
            });
        }
        [HttpPost]
        [Route("/staff/valid")]
        public bool Valid(string id)
        {
            return RegexHelper.IsMobile(id);
        }
        /// <summary>
        /// /账号不存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/staff/exist")]
        public async Task<bool> Exist(string id)
        {   var ids = id.Split('|');
            var key = ids[0].Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
            var staff = _staffService.Get(key.Trim());
            return await Task.FromResult(staff != null);
        }

        [Route("/staff/tree")]
        public async Task<JsonResult> CreateMenu()
        {
            var orgId = _orgService.GetList(LoginInfo.Id).OrgId;
            var orgs = _orgService.GetList();
            var menus = new List<MenuModel>();
            CreateMenu(menus, orgs, 0);
            return await Task.FromResult(Json(menus));
        }

        private void CreateMenu(List<MenuModel> menus, List<OrgDto> orgs, int pid)
        {
            var children = orgs.Where(m => m.ParentId == pid).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = new MenuModel { Id = child.Id.ToString(), Text = child.Name };
                model.State = "open";
                menus.Add(model);
                CreateMenu(model.Children, orgs, child.Id);
            }
        }

        #region  按照当前用户的角色显示所在组菜单
        //public async Task<JsonResult> CreateMenu()
        //{
        //    var orgId = _orgService.GetList(LoginInfo.Id).OrgId;
        //    var orgs = _orgService.GetList().Where(m => m.ParentId == orgId || m.Id == orgId).OrderBy(p => p.OrderBy).ToList();
        //    var menus = new List<MenuModel>();
        //    foreach (var child in orgs)
        //    {
        //        var model = new MenuModel { Id = child.Id.ToString(), Text = child.Name, State = "open" };
        //        menus.Add(model);
        //       // CreateMenu(model.Children, orgs, child.Id);
        //    }
        //    //CreateMenu(menus, orgs, orgId);
        //    return await Task.FromResult(Json(menus));
        //}

        //private void CreateMenu(List<MenuModel> menus, List<OrgDto> orgs, int orgId)
        //{
        //    var children = orgs.Where(m => m.ParentId == orgId || m.Id== orgId).OrderBy(p => p.OrderBy).ToList();
        //    foreach (var child in children)
        //    {
        //        var model = new MenuModel {Id = child.Id.ToString(), Text = child.Name, State = "open"};
        //        menus.Add(model);
        //        CreateMenu(model.Children, orgs, child.Id);
        //    }
        //}
        #endregion

    }
}