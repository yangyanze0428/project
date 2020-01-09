using AutoMapper;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;

namespace Domain.Common.Profiles
{
    public class OrgProfile : Profile
    {
        public OrgProfile()
        {
            CreateMap<OrgDto, OrgEntity>();
            CreateMap<OrgEntity, OrgDto>();
        }
    }
}
