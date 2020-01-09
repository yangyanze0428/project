using Domain.Common.Dtos.App;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class AppProfile: AutoMapper.Profile
    {
        public AppProfile()
        {
            CreateMap<AppDto, AppModel>()
                .ForMember(d => d.AppType, opt => opt.MapFrom(s => s.AppType.GetDescription()))
                .ForMember(d => d.OperateDate, opt => opt.MapFrom(s => s.OperateDate.ToString19()))
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
