using AutoMapper;
using Domain.Common.Dtos.Order;
using Hx.Extensions;
using Web.CusManager.Models;

namespace Web.CusManager.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, OrderModel>()
                .ForMember(m => m.OrderTypeDesc, opt => opt.MapFrom(s => s.OrderType.GetDescription()))
                .ForMember(m => m.ProductTypeDesc, opt => opt.MapFrom(s => s.ProductType.GetDescription()))
                .ForMember(m => m.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
