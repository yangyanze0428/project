using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.IService;
using Hx.Components;
using Hx.Dfs;
using Hx.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class AuthentyController : ControllerBase
    {
        private IAuthentyService _authentyService;
        private ICustomerService _customerService;
        public AuthentyController(IAuthentyService authentyService, ICustomerService customerService)
        {
            _authentyService = authentyService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var customer = _customerService.Get(LoginInfo.Id);
            var authenty = _authentyService.Get(LoginInfo.Id);
            if (authenty.Id.IsNullOrEmpty())
            {
                ViewBag.AuditResult = "-";
            }
            else
            {
                ViewBag.AuditResult = customer.AuditResult.GetDescription();
            }
            return View();
        }

        public IActionResult Modify()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Modify(AuthentyDto dto, List<IFormFile> formFiles)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var files = Request.Form.Files;
                    var stream = files[0].OpenReadStream();
                    var br = new BinaryReader(stream);
                    var image = br.ReadBytes((int)stream.Length);
                    var memory = new MemoryStream(image) { Position = 0 };
                    var documentManager = ObjectContainer.Resolve<IDocumentManager>();
                    var streamId = documentManager.Upload($"Licence{DateTime.Now.ToString19()}", memory);
                    stream.Close();
                    br.Close();
                    memory.Close();

                    var name = Request.Form["Name"];
                    var telePhone = Request.Form["Telephone"];
                    var address = Request.Form["Address"];
                    var email = Request.Form["Email"];
                    var phone = Request.Form["Phone"];
                    var createDate = Request.Form["CreateDate"];
                    dto.Id = LoginInfo.Id;
                    dto.Licence = streamId;
                    dto.Name = name;
                    dto.Telephone = telePhone;
                    dto.Address = address;
                    dto.Email = email;
                    dto.Phone = phone;
                    dto.CreateDate = DateTime.Now;
                    dto.SalesMan = _customerService.Get(LoginInfo.Id).SalesMan;
                    var rlt = _authentyService.Add(dto,0);
                    return Json(rlt);
                }
                catch (Exception e)
                {
                    Logger.Error("Licence上传错误", e);
                    return Json(new Result(false, "服务器内部错误，请联系技术人员"));
                }
            });
        }
    }
}