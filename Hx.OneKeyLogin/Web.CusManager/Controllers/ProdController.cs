using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Dtos.Order;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Pay.Ali;

using Microsoft.AspNetCore.Mvc;

namespace Web.CusManager.Controllers
{
    public class ProdController : ControllerBase
    {
        private readonly IProdService _prodService;

        public ProdController(IProdService prodService)
        {
            _prodService = prodService;
        }

        public IActionResult Index()
        {
            //如果用户id等于当前用户或为空 查询显示   p = p.And(m => m.UserId == null || m.UserId.Equals(para.UserId));
            //否则 不显示

            //List<ProdDto> prod;
            //var para = new ProdPara
            //{
            //    UserId = LoginInfo.Id
            //};
            //if (userid.Equals(LoginInfo.Id))
            //{
            //    prod = _prodService.GetList(para,out var count);
            //}
            //else
            //{
            //    prod = _prodService.GetList(LoginInfo.Id); //不显示
            //}
            var prod = _prodService.GetList(LoginInfo.Id);
            return View(prod);
        }
        public IActionResult Modify(string id)
        {
            var b = _prodService.Get(id);
            //b.Value
            return View(b);
        }
    }
}