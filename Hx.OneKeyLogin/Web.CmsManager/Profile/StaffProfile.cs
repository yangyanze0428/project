using Domain.Common.Dtos.MemberShip;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class StaffProfile: AutoMapper.Profile
    {
        public StaffProfile()
        {
            CreateMap<StaffDto, StaffModel>()
                .ForMember(d => d.UserType, opt => opt.MapFrom(s => s.UserType.GetDescription()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.GetDescription()))
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
