using Domain.Common;
using Domain.Common.Dtos.Order;
using Domain.Common.Entities.Order;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Extensions;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
   public class OrderService:ServiceBase, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public OrderDto Get(string id)
        {
            var dto = UnitOfWorkService.Execute(() => _orderRepository.Get(id));
            if (dto == null) return null;
            return dto.MapTo<OrderDto>();
        }
        public List<OrderDto> GetList(OrderPara para, out int total)
        {
            try
            {
                var count = 0;
                var p = PredicateBuilder.Default<OrderEntity>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.Id.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.Id.Equals(para.Id));
                }
                if (para.Name.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.Name.Contains(para.Name));//应用名称
                }

                if (para.OrderType != 0)
                {
                    p = p.And(m => m.OrderType == (int)para.OrderType);//订单类型
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                var order = UnitOfWorkService.Execute(() =>
                {
                    var list = _orderRepository.GetPaged(p, para.PageNumber, para.PageSize, out count, false, m => m.CreateDate);
                    return list?.Select(c => c.MapTo<OrderDto>()).ToList();
                });
                if (order == null)
                {
                    total = 0;
                    return new List<OrderDto>();
                }
                total = count;
                return order;
            }
            catch (Exception e)
            {
                Logger.Error("select * from order error", e);
                total = 0;
                return new List<OrderDto>();
            }
        }
        public Result Add(OrderDto dto)
        {
            try
            {
                var list = dto.MapTo<OrderEntity>();
                var result = UnitOfWorkService.Execute(() => _orderRepository.InsertAndGetId(list));
                if (result.IsNullOrEmpty()) return new Result(false, "数据库操作失败");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error("Order Add", ex);
                return new Result(false, "添加失败");
            }
        }
        public Result Update(OrderDto dto)
        {
            try
            {
                var list = dto.MapTo<OrderEntity>();
                var result = UnitOfWorkService.Execute(() => _orderRepository.Update(list));
                if (!result) return new Result(false, "数据库操作失败");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error("Order update", ex);
                return new Result(false, "修改失败");
            }
        }
        public Result Delete(string id)
        {
            try
            {
                var result = UnitOfWorkService.Execute(() => _orderRepository.Delete(id));
                if (!result) return new Result(false, "操作数据库失败");
                return new Result(true, "");
            }
            catch (Exception ex)
            {
                Logger.Error("Order Delete", ex);
                return new Result(false, "删除失败");
            }
        }
    }
}
