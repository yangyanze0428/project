using Domain.Common.Dtos.Order;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class OrderProfile:AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, OrderModel>()
                .ForMember(d => d.ProductType, opt => opt.MapFrom(s => s.ProductType.GetDescription()))
                .ForMember(d => d.OrderType, opt => opt.MapFrom(s => s.OrderType.GetDescription()))
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
