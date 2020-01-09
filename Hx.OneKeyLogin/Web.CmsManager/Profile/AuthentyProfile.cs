using Domain.Common.Dtos.MemberShip;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class AuthentyProfile : AutoMapper.Profile
    {
        public AuthentyProfile()
        {
            CreateMap<AuthentyDto,AuthentyModel >()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
