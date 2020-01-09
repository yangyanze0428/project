using Hx.HttpClientFactory;
using Hx.Logging;
using Hx.Serializing;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Service.Handler
{
    public class CusNotificationHandler : INotificationHandler<CusNotification>
    {
        private readonly IHxHttpClientFactory _hxHttpClientFactory;
        private readonly ISerializer _serializer;
        private ILogger Logger { get; set; }
        private readonly IConfiguration _config;

        public CusNotificationHandler(IHxHttpClientFactory hxHttpClientFactory, ISerializer serializer,ILoggerFactory loggerFactory, IConfiguration config)
        {
            _hxHttpClientFactory = hxHttpClientFactory;
            _serializer = serializer;
            _config = config;
            Logger = loggerFactory.Create();
        }

        public Task Handle(CusNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var http = _hxHttpClientFactory.CreateHttpClient();
                var url = _config.GetValue("WebApi:Url", "http://192.168.1.160:15002/sync");
                http.SendAsync(url, _serializer.Serialize(notification));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                var json = _serializer.Serialize(notification);
                Logger.Error($"同步失败{e},json{json}");
                return Task.CompletedTask;
            }
        }
    }
}
