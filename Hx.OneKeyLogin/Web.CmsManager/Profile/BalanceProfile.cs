using Domain.Common.Dtos.Balance;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class BalanceProfile:AutoMapper.Profile
    {
        public BalanceProfile()
        {
            CreateMap<BalanceDto, BalanceModel>()
                .ForMember(d => d.ModifyDate, opt => opt.MapFrom(s => s.ModifyDate.ToString19()));
        }
    }
}
