using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IAuthentyService
    {
        Result Add(AuthentyDto dto, UserType roleId);
        Result Update(AuthentyDto dto);
        Result Delete(string id);
        AuthentyDto Get(string id);
        List<AuthentyDto> GetList(AuthentyPara para, out int total);
    }
}
