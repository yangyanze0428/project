using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class OrgService : ServiceBase, IOrgService
    {
        private IOrgRepository _repo;
        private IStaffRepository _staffRepository;
        public OrgService(IOrgRepository orgRepository, IStaffRepository staffRepository)
        {
            _repo = orgRepository;
            _staffRepository = staffRepository;
        }

        public List<OrgDto> GetList()
        {
            var list = UnitOfWorkService.Execute(() => _repo.GetList().ToList()).Where(m => m.IsEnable == 1);
            return list.Select(m => m.MapTo<OrgDto>()).ToList();
        }
        
        public StaffDto GetList(string salesMan) 
        {
            var list = UnitOfWorkService.Execute(() =>_staffRepository.GetOrgId(salesMan));
            return list.MapTo<StaffDto>();
        }

        public Result Update(OrgDto dto)
        {
            try
            {
                var entity = dto.MapTo<OrgEntity>();
                var rlt = UnitOfWorkService.Execute(() => _repo.Update(entity));
                if (rlt) return new Result(true, "");

                return new Result(false, "数据操作失败");
            }
            catch (Exception ex)
            {
                Logger.Error("update org error:", ex);
                return new Result(false, "内部服务器错误");
            }

        }

        public Result Add(OrgDto dto)
        {
            try
            {
                var entity = dto.MapTo<OrgEntity>();
                var rlt = UnitOfWorkService.Execute(() => _repo.InsertAndGetId(entity));
                if (rlt > 0) return new Result(true, "");

                return new Result(false, "数据操作失败");
            }
            catch (Exception ex)
            {
                Logger.Error("add org error:", ex);
                return new Result(false, "内部服务器错误");
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var entity = _repo.Get(id);
                    entity.IsEnable = 0;
                    var result = _repo.Update(entity);
                    return result;
                });
                if (rlt) return new Result(true, "");

                return new Result(false, "数据操作失败");
            }
            catch (Exception ex)
            {
                Logger.Error("delete org error:", ex);
                return new Result(false, "内部服务器错误");
            }
        }

        public OrgDto Get(int id)
        {
            var orgEntity = UnitOfWorkService.Execute(() => _repo.Get(id));
            if (orgEntity == null) return new OrgDto();
            return orgEntity?.MapTo<OrgDto>();
        }
    }
}
