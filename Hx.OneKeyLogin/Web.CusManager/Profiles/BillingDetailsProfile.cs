using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common.Dtos.Balance;
using Hx.Extensions;
using Web.CusManager.Models;


namespace Web.CusManager.Profiles
{
    public class BillingDetailsProfile: Profile
    {
        public BillingDetailsProfile()
        {
            CreateMap<BillingDetailsDto, BillingDetailsModel>()
                .ForMember(m => m.Value, opt => opt.MapFrom(s =>Math.Abs(s.Value)))
                .ForMember(m => m.ChargeType, opt => opt.MapFrom(s => s.ChargeType.GetDescription()))
                .ForMember(m => m.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
