using AutoMapper;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Perms;

namespace Domain.Common.Profiles
{
    public class DataInOrgProfile : Profile
    {
        public DataInOrgProfile()
        {
            CreateMap<DataInOrgDto, DataInOrgEntity>();
            CreateMap<DataInOrgEntity, DataInOrgDto>();
        }
    }
}
