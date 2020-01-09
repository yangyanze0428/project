using Autofac;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Service.Handler;
using Hx.Components;
using Hx.Components.Autofac;
using Hx.Configurations.Startup;
using Hx.Db.Oracle;
using Hx.Events.Bus;
using Hx.HttpClientFactory;
using Hx.Module.Oracle;
using Hx.Modules;
using Hx.MongoDb;
using Hx.ObjectMapping;
using Hx.Reflection;
using MediatR;
using System.Reflection;
using Web.CmsManager.Helper;
using Web.CmsManager.Models;

namespace Web.CmsManager
{
    [DependsOn(
        typeof(DomainModule)
         , typeof(OracleModule)
        , typeof(DapperMapperModule)
    //, typeof(MediatRModule)
    , typeof(MongoDbModule)
        , typeof(HttpClientModule)
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
            BuildMediator();
            //Register<INotificationHandler<Notification>, NotificationHandler>();
            //Register<IRequestHandler<HandlerModel, bool>, Handler>();

            //ObjectContainer.Register<DomainEventNodeConsumer>();
            AutoMapConfig();
            //ObjectContainer.Register<IStaffWebService>();

            // Register<IRequestHandler<SyncModel1, bool>,CustomerHandler>();

        }
        private static void BuildMediator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(CusNotification).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }


            // It appears Autofac returns the last registered types first


            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // var container = builder.Build();
            builder.Update(((AutofacObjectContainerEx)ObjectContainer.Current).Container);
        }
        public override void Initialize()
        {
            ConfigConsumerAndObserver();
            FindAndRegisterModel();


        }
        public override void BeforePostInitialize()
        {

        }
        private void ConfigConsumerAndObserver()
        {
            //var consumerConfig = Configuration.Modules.StartupConfiguration.Modules.Mq().ConsumerConfigurator;
            //consumerConfig.AddConsumer<DomainEventNodeConsumer>(cfg =>
            //{
            //    //cfg.PurgeOnStartup = true;
            //    cfg.Durable = false;
            //    cfg.AutoDelete = true;
            //    //cfg.UseRetry(c => { c.Interval(3, TimeSpan.FromSeconds(3));});
            //    //cfg.QueueExpiration = TimeSpan.FromSeconds(5);
            //});
        }
        public override void PostInitialize()
        {
            //TestManager.RegistAssembly(Assembly.GetExecutingAssembly());
            EventHandlerRegister();
        }
        void EventHandlerRegister()
        {
            var eventBus = ObjectContainer.Resolve<IEventBus>();
            InitEvetnBus(eventBus);
        }

        private void InitEvetnBus(IEventBus eventBus)
        {

        }
        void AutoMapConfig()
        {
            var autoMapperConfig =
                ObjectContainer.Resolve<IStartupConfiguration>().Modules.AutoMapper();
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
    }
}
