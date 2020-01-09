using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Hx.Configurations.Startup;
using Hx.Db.Oracle;
using Hx.HttpClientFactory;
using Hx.ICacheManager;
using Hx.Module.Oracle;

using Hx.Modules;
using Hx.MongoDb;
using Hx.ObjectMapping;
using Hx.Reflection;
using Microsoft.Extensions.Configuration;
using Web.CusManager.Helper;
using Web.CusManager.Models;
using Hx.Pay.Ali;

namespace Web.CusManager
{
    [DependsOn(
         typeof(DomainModule),
         typeof(OracleModule),
         typeof(DapperMapperModule),
         typeof(MongoDbModule),
         typeof(HttpClientModule)
        )]

    public class WebModule : ModuleBase
    {
        private readonly ITypeFinder _typeFinder;
        public WebModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }
        public override void PreInitialize()
        {
            AutoMapConfig();
        }
        public override void Initialize()
        {
            Resolve<ICustomerCache>().Init();
            FindAndRegisterModel();
            //AliPayConfig();
        }

        void AutoMapConfig()
        {
            var autoMapperConfig =
                Resolve<IStartupConfiguration>().Modules.AutoMapper();
            autoMapperConfig.Configurators.Add(config =>
            {

            });
        }
        private void FindAndRegisterModel()
        {
            var types = _typeFinder.Find(type => typeof(ViewModelBase).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
            foreach (var type in types)
            {
                ModelSchemeHelper.Register(type);
            }
        }
        private void AliPayConfig()
        {
            var appSetting = Resolve<IConfiguration>();
            Config.NotifyUrl = appSetting["aliPay:NotifyUrl"];
            Config.ReturnUrl = appSetting["aliPay:ReturnUrl"];
            Config.gatewayUrl = appSetting["aliPay:gatewayUrl"];
            //Config.private_key = appSetting["aliPay:private_key"];
            //Config.alipay_public_key = appSetting["aliPay:alipay_public_key"];
            Config.app_id = appSetting["aliPay:app_id"];
            Config.charset = appSetting["aliPay:charset"];
            Config.sign_type = appSetting["aliPay:sign_type"];
        }
    }
}
