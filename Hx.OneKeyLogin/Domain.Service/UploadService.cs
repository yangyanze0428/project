using Domain.Common;
using Domain.Common.Dtos.Upload;
using Domain.Common.Entities.Upload;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service
{
    public class UploadService : ServiceBase, IUploadService
    {
        private readonly IUploadRepository _uploadRepository;

        public UploadService(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        public UploadDto GetList()
        {
            var dto = UnitOfWorkService.Execute(() => _uploadRepository.GetList().ToList());
            var model = dto.FirstOrDefault();
            return model?.MapTo<UploadDto>();
        }

        public List<UploadDto> GetListUp()
        {
            var dto = UnitOfWorkService.Execute(() => _uploadRepository.GetList().ToList());
            return dto?.MapTo<List<UploadDto>>();
        }

        public Result Add(UploadDto dto)
        {
            try
            {
                var up = dto.MapTo<UploadEntity>();
                var rlt = UnitOfWorkService.Execute(() => _uploadRepository.InsertAndGetId(up));
                return rlt > 0 ? new Result(true, "") : new Result { Status = false };
            }
            catch (Exception ex)
            {
                Logger.Error($" add upload to exception:{ex}");
                return new Result { Status = false, Message = ex.Message };
            }
        }
    }
}
