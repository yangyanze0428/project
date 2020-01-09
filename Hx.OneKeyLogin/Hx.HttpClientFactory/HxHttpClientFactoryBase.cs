using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.HttpClientFactory
{
    public class HxHttpClientFactoryBase<IT, T> : IHxHttpClientFactory<IT, T> where T : class, IT
                                                    where IT : class
    {
        private IHost _host;
        public HxHttpClientFactoryBase()
        {
            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient();
                services.AddTransient<IT, T>();
            }).UseConsoleLifetime();
            _host = builder.Build();
        }
        public IT CreateHttpClient()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var service = serviceScope.ServiceProvider;
                return service.GetRequiredService<IT>();
            }
        }
    }
}
