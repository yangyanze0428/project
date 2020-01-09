using AutoMapper;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;

namespace Domain.Common.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, RoleEntity>();
            CreateMap<RoleEntity, RoleDto>();
        }
    }
}
