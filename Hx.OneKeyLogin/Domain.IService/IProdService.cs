using Domain.Common;
using Domain.Common.Dtos.Order;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IProdService
    {
        List<ProdDto> GetList(string userId);
        ProdDto Get(string id);
        ProdDto GetNumber(int money);
        List<ProdDto> GetList(ProdPara para, out int total);
        Result Add(ProdDto dto, UserType roleId);
        Result Update(ProdDto dto);
        Result Delete(string id);
    }
}
