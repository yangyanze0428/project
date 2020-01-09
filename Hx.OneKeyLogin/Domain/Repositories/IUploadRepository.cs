using Domain.Common.Entities.Upload;
using Hx.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Repositories
{
   public interface IUploadRepository : IRepository<UploadEntity, long>
    {
    }
}
