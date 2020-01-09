using Hx;
using Hx.Components;
using Hx.Configurations.Application;
using Hx.Domain.Events;
using Hx.Domain.Services;
using Hx.Logging;
using Hx.Serializing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    [PropertyWired]
    public abstract class ServiceBase
    {
        private ILogger _logger;

        public HxAppSettings AppSetting { get; set; }

        protected string NodeName => AppSetting.NodeName;

        /// <summary>
        /// 日志工厂
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// guid生成器
        /// </summary>
        public IGuidGenerator GuidGenerator { get; set; }

        /// <summary>
        /// Uow服务调用
        /// </summary>
        public IUnitOfWorkService UnitOfWorkService { get; set; }

        /// <summary>
        /// domain事件发布
        /// </summary>
        public IDomainEventHelper DomainEventHelper { get; set; }

        /// <summary>
        /// 序列化工具
        /// </summary>
        public ISerializer Serializer { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        //protected ILogger Logger => _logger ?? (_logger = LoggerFactory.Create(GetType()));
        protected ILogger Logger => _logger ?? (_logger = LoggerFactory.Create());
    }
}
