using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface IOrgService
    {
        List<OrgDto> GetList();
        OrgDto Get(int id);
        Result Delete(int id);
        Result Add(OrgDto dto);
        Result Update(OrgDto dto);
        StaffDto GetList(string salesMan);
    }
}
