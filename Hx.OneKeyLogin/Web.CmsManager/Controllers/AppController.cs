using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using Domain.IService;
using Hx;
using Hx.Components;
using Hx.Dfs;
using Hx.Extensions;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Web.CmsManager.Models;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class AppController : ControllerBase
    {
        private IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }
        [Route("/app")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("/app/getlist")]
        public async Task<JsonResult> GetList(string id, string userId, string appName, AppType appType, DateTime startTime, DateTime endTime, int page = 1, int rows = 20)
        {
            var _staffService = ObjectContainer.Resolve<IStaffService>();
            try
            {
                return await Task.Run(() =>
                {
                    var para = new AppPara
                    {
                        Id = id,
                        UserId = userId,
                        AppName = appName,
                        AppType = appType,
                        StartTime = startTime,
                        EndTime = endTime,
                        PageNumber = page - 1,
                        PageSize = rows,
                        OrgId = LoginInfo.Org.Id,
                        RoleId = LoginInfo.Role
                    };
                    var rlt = _appService.GetList(para, out var count);

                    var data = rlt.MapTo<IEnumerable<AppModel>>().ToList();
                    data.ForEach(m => m.Icon = m.Icon);
                    data.ForEach(m => m.SalesMan = _staffService.Get(m.SalesMan)?.Name);
                    var res = new JsonResult(new { total = count, rows = data });
                    return res;
                });
            }
            catch (Exception e)
            {
                Logger.Error("查询错误", e);
                return new JsonResult(new { total = 0, rows = "" });
            }
        }

        public IActionResult Img(string val)
        {
            try
            {
                if (val==null)
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
        [Route("/app/modi")]
        public IActionResult Modify(string opt, string id)
        {
            try
            {
                ViewBag.opt = opt;
                ViewBag.id = id;
                var model = new AppDto { MessageSecret = Util.GetRandomString(16) };
                if (!id.Equals("0"))
                {
                    model = _appService.Get(id);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error("app错误",ex);
                return View(new AppDto());
            }
        }
        [HttpPost]
        [Route("/app/modi")]
        public async Task<Result> Modify(AppDto dto, string opt)
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

                return await Task.Run(() =>
                {
                    dto.OperateDate = DateTime.Now;
                    dto.Operator = LoginInfo.Id;
                    if (opt == "add")//新增
                    {
                        dto.CreateDate = DateTime.Now;
                        dto.Status = 1;
                        dto.Creator = LoginInfo.Id;
                        return _appService.Add(dto,LoginInfo.Role);
                    }
                    //修改
                    return _appService.Update(dto);
                });
            }
            catch (Exception e)
            {
                Logger.Error("错误", e);
                return new Result { Status = false };
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            return await Task.Run(() => _appService.Delete(id));
        }
        [Route("/key")]
        public async Task<bool> KeyExist(string id)
        {
            var appkey = _appService.Get(id);
            return await Task.FromResult(appkey == null);
        }
    }
}