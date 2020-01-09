using AutoMapper;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;

namespace Domain.Common.Profiles
{
    public class PermsProfile : Profile
    {
        public PermsProfile()
        {
            CreateMap<PermsDto, PermsEntity>();
            CreateMap<PermsEntity, PermsDto>();
        }
    }
}
