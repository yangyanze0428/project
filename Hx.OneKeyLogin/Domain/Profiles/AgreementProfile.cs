using AutoMapper;
using Domain.Common.Dtos.Agreement;
using Domain.Common.Entities.Agreement;

namespace Domain.Common.Profiles
{
    public class AgreementProfile : Profile
    {
        public AgreementProfile()
        {
            CreateMap<AgreementDto, AgreementEntity>();
            CreateMap<AgreementEntity, AgreementDto>();
        }
    }
}
