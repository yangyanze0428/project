using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Perms;
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
    public class RoleController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetList()
        {
            return await Task.Run(() =>
            {
                var roleService = ObjectContainer.Resolve<IRoleService>();
                var roleList = roleService.GetRoleList().MapTo<List<RoleModel>>().ToList();
                return new JsonResult(new { total = roleList.Count, rows = roleList });
            });
        }

        public ActionResult Modify(string opt = "", string id = "")
        {
            ViewBag.opt = opt;
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Modify(RoleDto dto, string opt)
        {
            var roleService = ObjectContainer.Resolve<IRoleService>();
            if (opt == "add") //添加
            {
                var maxId = roleService.GetRoleList().Max(m => m.Id);
                dto.Id = (int.Parse(maxId) * 2).ToString();
                var addRlt = await Task.Run(() => roleService.InsertRole(dto));
                return addRlt;
            }

            //修改
            var updateRlt = await Task.Run(() => roleService.UpdateRole(dto));
            return updateRlt;
        }
        /// <summary>
        /// 获取单个数据 
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        public async Task<JsonResult> Get(string id)
        {
            return await Task.Run(() =>
            {
                var obj = ObjectContainer.Resolve<IRoleService>();
                var data = obj.GetRole(id);
                var d = data.MapTo<RoleModel>();
                return new JsonResult(new { rows = d });
            });
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            var roleService = ObjectContainer.Resolve<IRoleService>();
            return await Task.Run(() => roleService.DeleteRole(id));
        }

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetStaffList()
        {
            return await Task.Run(() =>
            {
                var staffService = ObjectContainer.Resolve<IStaffService>();
                var list = new List<IdText>
                {
                    new IdText {Id = "0", Text = "请选择", selected = true}
                };
                var rlt = staffService.GetStaff(LoginInfo.Role,LoginInfo.Org.Id);
                foreach (var item in rlt)
                {
                    var litem = new IdText { Text = item.Name, Id = item.Id };
                    list.Add(litem);
                }
                return Json(list);
            });
        }
        /// <summary>
        /// 给用户授角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="ids">角色id</param>
        /// <returns></returns>
        public async Task<Result> Accredit(string userId, string ids)
        {
            var userInRoleService = ObjectContainer.Resolve<IUserInRoleService>();
            if (userId.Equals("0"))
                return new Result(false, "用户不可为空");
            var userInRoleDto = new UserInRoleDto { UserId = userId };
            return await Task.Run(() =>
            {
                var rlt = new Result(true, "");
                foreach (var item in ids.Split(','))
                {
                    userInRoleDto.RoleId = item;
                    rlt = userInRoleService.AddUserInRole(userInRoleDto);
                }
                return rlt;
            });
        }
        /// <summary>
        /// 根据用户id 获取角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetRoleByUser(string userId)
        {
            return await Task.Run(() =>
            {
                var userInRoleService = ObjectContainer.Resolve<IUserInRoleService>();
                var rlt = userInRoleService.GetListByUserId(userId);
                return Json(rlt);
            });
        }
    }
}
