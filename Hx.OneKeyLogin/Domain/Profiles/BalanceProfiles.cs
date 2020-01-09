using Domain.Common.Dtos.Balance;
using Domain.Common.Entities.Balance;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common.Entities.Expenses;

namespace Domain.Common.Profiles
{
    public class BalanceProfiles:AutoMapper.Profile
    {
        public BalanceProfiles() 
        {
            CreateMap<BalanceDto,BalanceEntity>();
            CreateMap<BalanceEntity, BalanceDto>();
            CreateMap<BalanceDto, RechargeEntity>().ForMember(r => r.CreateDate, opt => opt.MapFrom(b => b.ModifyDate));
            CreateMap<RechargeEntity, RechargeDto>();
        }
    }
}
