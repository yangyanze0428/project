using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hx.Extensions;
using Hx.Utilities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web.CmsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = CommandLineArgumentParser.Parse(args);
            var p = parser.Get("--urls");
            var urls = p == null ? "" : p.Next;
            //CreateWebHostBuilder(args).Build().Run();
            IConfiguration cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables(evs => { })
                //.AddCommandLine()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                //.UseUrls(bindUrl)
                .UseStartup<Startup>()
                .UseConfiguration(cfg);
            if (urls.IsNotNullOrEmpty()) builder.UseUrls(urls);
            //CreateWebHostBuilder(args).Build
            var host = builder.Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
