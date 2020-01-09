using Domain.Common.Dtos.Balance;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class RechargeProfile:AutoMapper.Profile
    {
        public RechargeProfile()
        {
            CreateMap<RechargeDto, RechargeModel>()
                .ForMember(m => m.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(m => m.Bank, opt => opt.MapFrom(s => s.Bank.GetDescription()))
                .ForMember(m => m.TradeResult, opt => opt.MapFrom(s => s.TradeResult.GetDescription()))
                .ForMember(m => m.TradeType, opt => opt.MapFrom(s => s.TradeType.GetDescription()))
                .ForMember(m => m.IdenTity, opt => opt.MapFrom(s => s.IdenTity.GetDescription()))
                .ForMember(m => m.RechargeMode, opt => opt.MapFrom(s => s.RechargeMode.GetDescription()));
        }
    }
}
