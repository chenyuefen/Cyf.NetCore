using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cyf.AuthorizationServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // 1、ioc容器中添加IdentityServer4
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()// 1、用户登录配置
            //    .AddInMemoryApiResources(Config.GetApiResources()) // 2、存储Api资源
            //    .AddInMemoryClients(Config.GetClients()) // 3、存储客户端(模式)
            //    .AddTestUsers(Config.GetUsers())// 4、添加登录用户(模式)
            //    .AddInMemoryIdentityResources(Config.Ids); // 4、使用openid模式	;

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("CyfMSSQLConnection");
            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, options =>
                             options.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, options =>
                  options.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddTestUsers(Config.GetUsers())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // 1、将config中数据存储起来
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
