using DapperExtensions.Mapper;
using Domain.Common.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
   public class UploadMapper: ClassMapper<UploadEntity>
    {
        public UploadMapper()
        {
            Table("Login_Upload");
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
