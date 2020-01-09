using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hx.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.CusManager.Filters;

namespace Web.CusManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.Cookie.Name = "AuthCookieSms";
                options.Cookie.Path = "/";
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
            ConfigureAlipay(services);
            services.AddMvc(options =>{
                options.Filters.Add<HttpGlobalExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            Hx.BootStrapHelper.Boot<WebModule>(builder =>
            {
                builder.Populate(services);
            }, "plugins");
            ApplicationContainer = ((Hx.Components.Autofac.AutofacObjectContainerEx)ObjectContainer.Current).Container;
            return new AutofacServiceProvider(ApplicationContainer);
        }
        private void ConfigureAlipay(IServiceCollection services)
        {
            var alipayOptions = Configuration.GetSection("Alipay").Get<AlipayOptions>();
            services.AddAlipay(options => options.SetOption(alipayOptions));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();//登录权限认证
            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500;
            //        if (context.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            //        {
            //            context.Response.ContentType = "text/html";
            //            await context.Response.SendFileAsync($@"{env.WebRootPath}/errors/500.html");
            //        }
            //    });
            //});
            //app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
