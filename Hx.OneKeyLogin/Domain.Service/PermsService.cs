using Domain.Common.Dtos.Perms;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.Components;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
    public class PermsService : ServiceBase, IPermsService
    {
        private readonly IPermsRepository _permrepo;
        public PermsService(IPermsRepository permrepo)
        {
            _permrepo = permrepo;
        }
        public bool DeletePerms(string id)
        {
            throw new NotImplementedException();
        }

        public string GetList(int platFormType, int isenable)
        {
            throw new NotImplementedException();
        }

        public PermsDto GetPerms(string id)
        {
            throw new NotImplementedException();
        }

        public List<PermsDto> GetPermsList(int platFormType)
        {
            var result = UnitOfWorkService.Execute(() =>
                ObjectContainer.Resolve<IPermsRepository>().GetList().ToList());
            var permsList = result.Where(m => m.PlatFormType == platFormType).Select(m => m.MapTo<PermsDto>()).ToList();
            return permsList;
        }

        public List<PermsDto> GetPermsListById(string permIds)
        {
            if (string.IsNullOrWhiteSpace(permIds))
                return new List<PermsDto>();
            var perms = permIds.Split(',');//字符串分割
            var result = new List<PermsDto>();
            var permsList = GetPermsList(1);//TODO 平台类型的读取
            foreach (var item in perms)
            {
                result.Add(permsList.FirstOrDefault(m => m.Id == item));
            }
            return result;
        }

        public List<PermsDto> GetPermsListByUserId(string userId)
        {
            return UnitOfWorkService.Execute(() => _permrepo.GetPerms(userId).Where(m => m.IsEnable == 1).Select(m => m.MapTo<PermsDto>()).ToList());
        }

        public List<PermsDtoShow> GetPermsPara(PermsPara para, out int total)
        {
            throw new NotImplementedException();
        }

        public bool InsertPerms(PermsDto dto)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePerms(PermsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
