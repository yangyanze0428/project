using AutoMapper;
using Domain.Common.Dtos.App;
using Domain.Common.Entities.Application;

namespace Domain.Common.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<AppDto, AppEntity>();
            CreateMap<AppEntity, AppDto>();
        }
    }
}
