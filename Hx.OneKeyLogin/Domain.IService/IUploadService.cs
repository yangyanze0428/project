using Domain.Common;
using Domain.Common.Dtos.Upload;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
   public interface IUploadService
   {
       UploadDto GetList();
      List<UploadDto> GetListUp();
       Result Add(UploadDto dto);
   }
}
