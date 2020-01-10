using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Local.Api30
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //XmlDocument log4netConfig = new XmlDocument();
            //log4netConfig.Load(File.OpenRead("Log4net.config"));

            //var repo = log4net.LogManager.CreateRepository(
            //    Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            //log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
