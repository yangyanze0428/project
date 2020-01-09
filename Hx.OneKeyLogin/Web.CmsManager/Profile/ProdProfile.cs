using Domain.Common.Dtos.Order;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class ProdProfile: AutoMapper.Profile
    {
        public ProdProfile()
        {
            CreateMap<ProdDto, ProdModel>()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
