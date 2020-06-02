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
            //    .AddDeveloperSigningCredential()//��ʱ֤�飬AddSigningCredential��ʽ֤��
            //    .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())//�û�
            //    .AddInMemoryClients(InMemoryConfiguration.GetClients())//�ͻ���
            //    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());//��Щ�ӿ�

            // 1��ioc���������IdentityServer4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()// 1���û���¼����
                .AddInMemoryApiResources(Config.GetApiResources()) // 2���洢Api��Դ
                .AddInMemoryClients(Config.GetClients()) // 3���洢�ͻ���(ģʽ)
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

            // 1��ʹ��IdentityServe4
            app.UseIdentityServer();
            //2����Ӿ�̬��Դ����
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
