using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.IService;
using Hx;
using Hx.Components;
using Hx.Dfs;
using Hx.Extensions;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{
    [Authorize]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<JsonResult> GetList(AppPara para)
        {
            para.PageNumber = para.PageNumber - 1;
            para.UserId = LoginInfo.Id;
            return await Task.Run(() =>
            {
                var dtos = _appService.GetList(para, out var count);
                var list = dtos.MapTo<List<AppModel>>();
                return Json(new { rows = list, total = count });
            });
        }

        public IActionResult Modify(string id)
        {
            var dto = new AppDto();
            if (id.IsNotNullOrEmpty())
            {
                dto = Get(id);
                dto.AppTypeDesc = dto.AppType.GetDescription();
            }
            return View(dto);
        }

        [HttpPost]
        public async Task<Result> Modify(AppDto dto)
        {
            try
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var stream = files[0].OpenReadStream();
                    // var fileStream = new FileStream(name, FileMode.Open, FileAccess.Read);
                    var br = new BinaryReader(stream);
                    var image = br.ReadBytes((int)stream.Length);
                    var memory = new MemoryStream(image) { Position = 0 };
                    var documentManager = ObjectContainer.Resolve<IDocumentManager>();
                    var streamId = documentManager.Upload($"Picture_{DateTime.Now.ToString19()}", memory);
                    stream.Close();
                    br.Close();
                    memory.Close();
                    dto.Icon = streamId;
                }
                var rlt = new Result();
                dto.Operator = LoginInfo.Id;
                dto.OperateDate = DateTime.Now;
                if (dto.Id.IsNullOrEmpty())
                {
                    dto.Id = ObjectContainer.Resolve<IGuidGenerator>().Create().ToString();
                    dto.UserId = LoginInfo.Id;
                    dto.Creator = LoginInfo.Id;
                    dto.CreateDate = DateTime.Now;
                    //rlt = await Task.Run(() => _appService.Add(dto));
                }
                rlt = await Task.Run(() => _appService.Update(dto));
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("应用图片上传错误:", ex);
                return new Result{Status=false,Message=ex.ToString()};
            }
        }

        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() =>_appService.Delete(id));
        }

        public AppDto Get(string id)
        {
            var dto = _appService.Get(id);
            return dto;
        }

        public IActionResult Img(string val)
        {
            try
            {
                if (val == null)
                {
                    return null;
                }
                var documentManager = ObjectContainer.Resolve<IDocumentManager>();
                var memory = new MemoryStream();
                documentManager.DownloadToStream(val, memory);
                memory.Position = 0;
                var br = new BinaryReader(memory);
                var image = br.ReadBytes((int)memory.Length);
                //var map = new Bitmap(200, 200);
                //map.Save(memory, ImageFormat.Jpeg);
                var bytes = memory.GetBuffer();
                memory.Close();
                return File(image, "image/Jpeg");
            }
            catch (Exception e)
            {
                Logger.Error("图加载错误", e);
                return null;
            }
        }
    }
}