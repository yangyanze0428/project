using AutoMapper;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;

namespace Domain.Common.Profiles
{
    public class RoleInPermProfile : Profile
    {
        public RoleInPermProfile()
        {
            CreateMap<RoleInPermDto, RoleInPermEntity>();
            CreateMap<RoleInPermEntity, RoleInPermDto>();
        }
    }
}
