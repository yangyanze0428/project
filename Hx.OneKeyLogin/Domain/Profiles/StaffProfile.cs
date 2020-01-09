using AutoMapper;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;

namespace Domain.Common.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<StaffDto, StaffEntity>();
            CreateMap<StaffEntity, StaffDto>();
        }
    }
}
