using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.Upload;
using Domain.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Web.CusManager.Controllers
{
    public class UploadController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadService _uploadService;
        public UploadController(IHostingEnvironment hostingEnvironment, IUploadService uploadService)
        {
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
        }

        public IActionResult Index()
        {
            ViewBag.upload = _uploadService.GetListUp().OrderBy(m=>m.OrderBy);
            return View();
        }
        [HttpPost]
        [Route("/upload")]
        [RequestSizeLimit(100_000_000)]
        public Result Upload(UploadDto dto, List<IFormFile> formFiles)
        {
            try
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var file = files[0];
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var contentRootPath = _hostingEnvironment.ContentRootPath;
                    var filePath = webRootPath + @"/upload/" + file.FileName;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }

                    dto.Route = filePath;
                    dto.Name = Path.GetFileNameWithoutExtension(filePath); 
                    dto.FileName = Path.GetExtension(file.FileName);
                }
                var result = _uploadService.Add(dto);
                if (result.Status)
                {
                    result.Message = "/Upload/Index";
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"错误描述,{ex}");
                return new Result { Status = false };
            }

        }

        public IActionResult Download(string name, string fileName)
        {
            try
            {
                var webRootPath = _hostingEnvironment.WebRootPath + @"/upload/" + name + fileName;
                var fs = System.IO.File.Open(webRootPath, FileMode.Open);
                var provider = new FileExtensionContentTypeProvider();
                var mime = provider.Mappings[fileName];
                fs.Position = 0;
                return File(fs, mime, Path.GetFileName(name + fileName));
            }
            catch (Exception ex)
            {
                Logger.Error($"下载错误,{ex}");
                return null;
            }
        }

        public IActionResult Modify()
        {
            return View();
        }
    }
}