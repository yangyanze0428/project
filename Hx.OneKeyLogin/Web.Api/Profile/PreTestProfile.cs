using Domain.Common.Dtos.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Profile
{
    public class PreTestProfile:AutoMapper.Profile
    {
        public PreTestProfile() 
        {
            CreateMap<PreTestModel,PreTestDto>();
        }
    }
}
