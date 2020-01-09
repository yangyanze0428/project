using Domain.Common.Dtos.Balance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hx.Extensions;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class BillingDetailsProfile : AutoMapper.Profile
    {
        public BillingDetailsProfile()
        {
            CreateMap<BillingDetailsDto, BillingDetailsModel>()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(d => d.ChargeType, opt => opt.MapFrom(s => s.ChargeType.GetDescription()))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => Math.Abs(s.Value)));
        }
    }
}
