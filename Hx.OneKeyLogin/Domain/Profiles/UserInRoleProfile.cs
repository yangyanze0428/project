using AutoMapper;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;

namespace Domain.Common.Profiles
{
    public class UserInRoleProfile : Profile
    {
        public UserInRoleProfile()
        {
            CreateMap<UserInRoleDto, UserInRoleEntity>();
            CreateMap<UserInRoleEntity, UserInRoleDto>();
        }
    }
}
