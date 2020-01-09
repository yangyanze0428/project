using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.CmsManager.Controllers
{
    public class TabController : Controller
    {
        public IActionResult Index(string id, string src)
        {
            ViewBag.Id = id;
            ViewBag.Src = src;
            return View();
        }
    }
}