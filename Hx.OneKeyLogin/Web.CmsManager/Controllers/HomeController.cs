using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Perms;
using Domain.IService;
using Hx.Components;
using Hx.Serializing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View(LoginInfo);
        }
        [Route("/menu")]
        public async Task<List<MenuModel>> CreateMenu()
        {
            var obj = ObjectContainer.Resolve<IPermsService>();
            var perms = obj.GetPermsListByUserId(LoginInfo.Id);//根据当前登录用户，动态加载左侧菜单
            var menus = new List<MenuModel>();
            var serializer = ObjectContainer.Resolve<ISerializer>();
            if (serializer != null && perms == null)
            {
                Logger.Info(serializer.Serialize(perms));
            }
            CreateMenu(menus, perms, "0");

            return await Task.FromResult(menus);

        }
        private void CreateMenu(List<MenuModel> menus, List<PermsDto> perms, string pid)
        {
            var children = perms.Where(m => m.ParentId.Equals(pid)).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = new MenuModel { Id = child.Id, Text = child.Caption };
                if (!child.Module.Equals("-"))
                {
                    model.Attributes.Add("url", child.Module);
                    model.State = "open";
                }

                menus.Add(model);
                CreateMenu(model.Children, perms, child.Id);
            }
        }
        #region 权限管理
        public ActionResult About(string roleId)
        {
            var obj = ObjectContainer.Resolve<IRoleService>();
            ViewBag.roleId = roleId;
            var roleName = obj.GetRole(roleId).RoleName;
            ViewBag.roleName = roleName;
            return View();
        }

        /// <summary>
        /// 根据角色id 获取菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetPermsByRole(string roleId)
        {
            var obj = ObjectContainer.Resolve<IRoleInPermService>();
            var rlt = obj.GetPermsByRole(roleId);
            return Json(rlt);
        }

        /// <summary>
        /// 获取所有可用权限的树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTree()
        {
            //过滤禁用账户
            var obj = ObjectContainer.Resolve<IPermsService>();
            var treeList = obj.GetPermsList(0).Where(m => m.IsEnable == 1).ToList();
            var menus = new List<PermsDtoShow>();
            CreateMenu(menus, treeList, "0");
            return Json(menus);
        }

        private void CreateMenu(List<PermsDtoShow> menus, List<PermsDto> perms, string pid)
        {
            var children = perms.Where(m => m.ParentId.Equals(pid)).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = new PermsDtoShow
                {
                    Id = child.Id,
                    Caption = child.Caption,
                    Module = child.Module,
                    ParentId = child.ParentId,
                    Description = child.Description,
                    IsEnable = child.IsEnable
                };
                menus.Add(model);
                CreateMenu(model.Children, perms, child.Id);
            }

        }
        /// <summary>
        /// 给角色授权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="ids">权限id</param>
        /// <returns></returns>
        [Obsolete]
        public  Result Accredit(string roleId, string ids)
        {
            try
            {
                if (roleId.Equals("0"))
                    return new Result(false,"");
                var roleInPermDto = new RoleInPermDto { RoleId = roleId };
                var obj = ObjectContainer.Resolve<IRoleInPermService>();
                var rlt = new Result(true,"");
                foreach (var item in ids.Split(','))
                {
                    roleInPermDto.PermId = item;
                    rlt =obj.InsertRoleInPerm(roleInPermDto);
                }
                return rlt;
            }
            catch (Exception e)
            {
                Logger.Error("角色授权失败",e);
                return new Result(false,"");
            }
        }
        #endregion
        public IActionResult Show()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
