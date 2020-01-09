using DapperExtensions.Mapper;
using Domain.Common.Entities.MemberShip;

namespace Hx.Db.Oracle
{
    public class OrgMapper : ClassMapper<OrgEntity>
    {
        public OrgMapper()
        {
            Table("LOGIN_ORG");
            //Map(x => x.ConfigSetting).Ignore();
            Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
