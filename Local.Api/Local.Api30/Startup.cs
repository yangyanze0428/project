using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Local.Api.IService;
using Local.Api30.Extensions;
using Local.Api30.PolicyRequirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using static Local.Api30.Enums.ApiVersions;

namespace Local.Api30
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ApiName { get; set; } = "Local.Api";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //�Զ������
            services.AddAuthorization(m =>
            {
                //1 ���ڽ�ɫ
                m.AddPolicy("AdminPolicy1", m =>
                {//ClaimType=Role
                    m.RequireRole("Admin").Build();
                });
                //2 ��������Claim
                m.AddPolicy("AdminClaim2", m =>
                {//ClaimType
                    m.RequireClaim("Role", "Admin", "User");
                    m.RequireClaim("Email", "Admin@123.com", "User@456.cn");
                });
                //3 ��������Requirement
                m.AddPolicy("AdminRequireMent", m =>
                {//�Զ������
                    m.Requirements.Add(new AdminRequirement() { Name = "Yangyanze" });
                });
            });

            services.AddSingleton<IAuthorizationHandler, MustRoleAdminHandler>();//����ע��
            #region Bearer
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"));
            services.AddAuthentication("Bearer")
                .AddJwtBearer(m =>
                {
                    m.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,

                        ValidateIssuer = true,
                        ValidIssuer = "Local.Api30",

                        ValidateAudience = true,
                        ValidAudience = "wr",

                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                });
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //ע��Ҫͨ�����䴴�������
            var servicesDllFile = Path.Combine(basePath, "Local.Api.Service.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=Index}/{id?}");
            });
        }
    }
}
