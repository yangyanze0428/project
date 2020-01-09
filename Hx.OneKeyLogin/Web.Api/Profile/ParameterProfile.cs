using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common.Dtos.App;

namespace Web.Api.Profile
{
    public class ParameterProfile: AutoMapper.Profile
    {
        public ParameterProfile() 
        {
            CreateMap<ParameterProfile, Params>();
        }
    }
}
