using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Cyf.MicroService.Core.Registry;
using Cyf.MicroService.Core.Registry.Extentions;
using Cyf.MicroService.TeamService.Context;
using Cyf.MicroService.TeamService.Repositories;
using Cyf.MicroService.TeamService.Services;
using IdentityServer4.AccessTokenValidation;

namespace Cyf.MicroService.TeamService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 6、校验AccessToken,从身份校验中心进行校验
            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //        .AddIdentityServerAuthentication(options => {
            //            options.Authority = "http://localhost:5005"; // 1、授权中心地址
            //            options.ApiName = "TeamService"; // 2、api名称(项目具体名称)
            //            options.RequireHttpsMetadata = false; // 3、https元数据，不需要
            //        });
            // 1、注册上下文到IOC容器
            //services.AddDbContext<TeamContext>(options => {
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //});
            // 2、注册团队service
            services.AddScoped<ITeamService, TeamServiceImpl>();

            // 3、注册团队仓储
            services.AddScoped<ITeamRepository, TeamRepository>();

            // 4、添加映射
            //services.AddAutoMapper();

            // 5、添加服务注册条件
            services.AddConsulRegistry(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            // 1、增加样式
            app.UseStaticFiles();
            // 1、consul服务注册
            app.UseConsulRegistry();

            app.UseRouting();
            // 1、使用身份验证	
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
