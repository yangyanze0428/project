using DapperExtensions.Mapper;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Entities.Perms;

namespace Hx.Db.Oracle
{
    public class DataInOrgMapper : ClassMapper<DataInOrgEntity>
    {
        public DataInOrgMapper()
        {
            Table("Login_DataInOrg");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
