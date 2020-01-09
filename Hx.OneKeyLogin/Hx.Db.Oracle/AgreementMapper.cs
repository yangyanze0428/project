using DapperExtensions.Mapper;
using Domain.Common.Entities.Agreement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.Db.Oracle
{
    public class AgreementMapper:ClassMapper<AgreementEntity>
    {
        public AgreementMapper()
        {
            Table("Login_Agreement");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
