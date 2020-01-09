using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Components;
using Hx.Dfs;
using Hx.Extensions;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class AuthentyController : ControllerBase
    {
        private IAuthentyService _authentyService;
        private ICustomerService _customerService;
        private IStaffService _staffService;
        public AuthentyController(IAuthentyService authentyService,ICustomerService customerService, IStaffService staffService)
        {
            _authentyService = authentyService;
            _customerService = customerService;
            _staffService = staffService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetList(AuthentyPara para, int page = 0, int rows = 20)
        {
            try
            {
                return await Task.Run(() =>
                {
                    para.PageNumber = page - 1;
                    para.PageSize = rows;
                    para.OrgId = LoginInfo.Org.Id;
                    para.RoleId = LoginInfo.Role;
                    var rlt = _authentyService.GetList(para, out var count);
                    var data = rlt.MapTo<List<AuthentyModel>>();
                    data.ForEach(m => m.AuditResult = _customerService.Get(m.Id).AuditResult);
                    data.ForEach(m => m.SalesMan = _staffService.Get(m.SalesMan)?.Name);
                    data.ForEach(m => m.AuditResultDesc = _customerService.Get(m.Id)?.AuditResult.GetDescription());
                    return new JsonResult(new { total = count, rows = data });
                });
            }
            catch (Exception e)
            {
                Logger.Error("authentyController GetList Error", e);
                return new JsonResult(new { total = 0, rows = "" });
            }
        }

        public IActionResult Modify(string opt,string id)
        {
            ViewBag.opt = opt;
            ViewBag.customer = GetCustomerList();
            var model = new AuthentyDto();
            if (opt.Equals("update"))
            {
                model = _authentyService.Get(id);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<Result> Modify(AuthentyDto dto,string opt)
        {
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var stream = files[0].OpenReadStream();
                var br = new BinaryReader(stream);
                var image = br.ReadBytes((int)stream.Length);
                var memory = new MemoryStream(image) { Position = 0 };
                var documentManager = ObjectContainer.Resolve<IDocumentManager>();
                var streamId = documentManager.Upload($"Licence_{DateTime.Now.ToString19()}", memory);
                stream.Close();
                br.Close();
                memory.Close();
                dto.Licence = streamId;
            }
            return await Task.Run(() =>
            {
                if (opt.Equals("add"))//新增
                {
                    dto.CreateDate = DateTime.Now;
                    return _authentyService.Add(dto,LoginInfo.Role);
                }
                //修改
                return _authentyService.Update(dto);
            });
        }

        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() => _authentyService.Delete(id));
        }
        
        public IActionResult Audit(string id)
        {
            var authenty = _authentyService.Get(id);
            return View(authenty);
        }
        [HttpPost]
        public async Task<List<string>> Audit(string[] msgs, AuditResult result)
        {
            return await Task.Run(() => {
                var rlt = _customerService.Audit(msgs, result);
                return rlt;
            });
        }

        public SelectList GetCustomerList()
        {
            var list = new List<SelectListItem>();
            var rlt = _customerService.GetList().Where(m => m.Status == AccountStatus.Normal).ToList();
            foreach (var item in rlt)
            {
                var text = item.Id;
                var id = item.Id;
                list.Add(new SelectListItem { Text = text, Value = id });
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}