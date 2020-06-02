using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyf.AuthorizationServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cyf.AuthorizationServer
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
            //InMemoryConfiguration.Configuration = this.Configuration;
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//临时证书，AddSigningCredential正式证书
            //    .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())//用户
            //    .AddInMemoryClients(InMemoryConfiguration.GetClients())//客户端
            //    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());//那些接口

            // 1、ioc容器中添加IdentityServer4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()// 1、用户登录配置
                .AddInMemoryApiResources(Config.GetApiResources()) // 2、存储Api资源
                .AddInMemoryClients(Config.GetClients()) // 3、存储客户端(模式)
                .AddTestUsers(Config.GetUsers());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 1、使用IdentityServe4
            app.UseIdentityServer();
            //2、添加静态资源访问
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
