using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Service.MediatHandler;
using Hx.Configurations.Application;
using Hx.Db.Oracle;
using Hx.HttpClientFactory;
using Hx.Module.MediatR;
using Hx.Module.Oracle;
using Hx.Modules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Service.Handler;
using Web.Api.helpers;

namespace Web.Api
{
    [DependsOn(
        typeof(DomainModule)
         , typeof(OracleModule)
        , typeof(DapperMapperModule)
        ,typeof(MediatRModule)
        ,typeof(HttpClientModule)
    )]
    public class WebModule : ModuleBase
    {
        private readonly HxAppSettings _settings;

        public WebModule(HxAppSettings settings)
        {
            _settings = settings;
        }
        public override void PreInitialize()
        {
            Register<IValidParameter, ValidParameter>();
            Register<IFoundationVerification, FoundationVerification>();
            Register<INotificationHandler<SendCompleteCommand>, SendCompleteChargingHandler>();
            Register<INotificationHandler<SendCompleteCommand>, SendCompleteRedisChargingHandler>();
            Register<INotificationHandler<SendCompleteCommand>, SendCompleteExpenseDetailHandler>();
            //Register<CustomerHandler>();
            //ObjectContainer.Register<IStaffWebService>();
        }
        public override void Initialize()
        {



        }
        private void ConfigConsumerAndObserver()
        {

        }
        public override void PostInitialize()
        {
            //TestManager.RegistAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
