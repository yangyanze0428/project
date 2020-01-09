using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Common.Dtos.Upload;
using Domain.Common.Entities.Upload;

namespace Domain.Common.Profiles
{
    public class UploadProfile : Profile
    {
        public UploadProfile()
        {
            CreateMap<UploadDto, UploadEntity>();
            CreateMap<UploadEntity, UploadDto>();
        }
    }
}
