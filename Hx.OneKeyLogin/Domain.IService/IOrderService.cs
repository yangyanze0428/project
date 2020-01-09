using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using Domain.Common.Dtos.Order;

namespace Domain.IService
{
    public interface IOrderService
    {
        OrderDto Get(string id);
        List<OrderDto> GetList(OrderPara para, out int total);
        Result Add(OrderDto dto);
        Result Update(OrderDto dto);
        Result Delete(string id);
    }
}
