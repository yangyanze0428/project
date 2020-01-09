using AutoMapper;
using Domain.Common.Dtos.App;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CusManager.Models;

namespace Web.CusManager.Profiles
{
    public class AppProfile: Profile
    {
        public AppProfile()
        {
            CreateMap<AppDto, AppModel>()
                .ForMember(m => m.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(m => m.OperateDate, opt => opt.MapFrom(s => s.OperateDate.ToString19()));

        }
    }
}
