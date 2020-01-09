using AutoMapper;
using Domain.Common.Dtos.Order;
using Domain.Common.Entities.Order;

namespace Domain.Common.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, OrderEntity>();
            CreateMap<OrderEntity, OrderDto>();
        }
    }
}
