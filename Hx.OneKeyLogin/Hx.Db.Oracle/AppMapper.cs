using DapperExtensions.Mapper;
using Domain.Common.Entities.Application;

namespace Hx.Db.Oracle
{
    public class AppMapper : ClassMapper<AppEntity>
    {
        public AppMapper()
        {
            Table("Login_App");
            //Map(x => x.ConfigSetting).Ignore();
            //Map(x => x.Id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}
