using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.IService;
using Hx.Extensions;
using Hx.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Web.CmsManager.Controllers
{
    public class OrgController : ControllerBase
    {
        private IOrgService _orgService;
        public OrgController(IOrgService orgService)
        {
            _orgService = orgService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Modify(int id)
        {
            var model = new OrgDto();
            if (id!=0)
            {
                model = Get(id);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<Result> Modify(OrgDto dto)
        {
            if (dto.Id==0)//新增
            {
                dto.IsEnable = 1;
                dto.OrderBy = 0;
                return await Task.Run(() => _orgService.Add(dto));
            }
            //修改
            return await Task.Run(() => _orgService.Update(dto));
        }

        [Route("/tree")]
        public async Task<JsonResult> GetTree()
        {
            var treeList = _orgService.GetList();
            var orgShowList = new List<OrgDto>();
            CreateMenu(orgShowList, treeList, 0);
            return await Task.Run(() => Json(orgShowList));
        }

        private void CreateMenu(List<OrgDto> menus, List<OrgDto> orgs, int pid)
        {
            var children = orgs.Where(m => m.ParentId.Equals(pid)).OrderBy(p => p.OrderBy).ToList();
            foreach (var child in children)
            {
                var model = child.MapTo<OrgDto>();
                menus.Add(model);
                CreateMenu(model.Children, orgs, child.Id);
            }
        }

        public OrgDto Get(int id)
        {
            var org = _orgService.Get(id);
            return org;
        }

        [Route("/orgv")]
        public async Task<bool> Valid(int id)
        {
            return await Task.Run(() =>
            {
                var user = Get(id);
                return user == null;
            });
        }

        public async Task<Result> Delete(int id)
        {
            return await Task.Run(() =>  _orgService.Delete(id));
        }
    }
}