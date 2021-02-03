using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyf.AuthorizationServer.Models;
using Cyf.IdentityServer4.Context;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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
            // 1��ioc���������IdentityServer4
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()// 1���û���¼����
            //    .AddInMemoryApiResources(Config.GetApiResources()) // 2���洢Api��Դ
            //    .AddInMemoryClients(Config.GetClients()) // 3���洢�ͻ���(ģʽ)
            //    .AddTestUsers(Config.GetUsers())// 4����ӵ�¼�û�(ģʽ)
            //    .AddInMemoryIdentityResources(Config.Ids); // 4��ʹ��openidģʽ	;

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //var connectionString = Configuration.GetConnectionString("CyfMSSQLConnection");
            var connectionString = Configuration.GetConnectionString("CyfMYSQLConnection");
            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
						//builder.UseSqlServer(connectionString, options =>
						//     options.MigrationsAssembly(migrationsAssembly));
						builder.UseMySQL(connectionString, options =>
							 options.MigrationsAssembly(migrationsAssembly));
					};
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
						//builder.UseSqlServer(connectionString, options =>
						//    options.MigrationsAssembly(migrationsAssembly));
						builder.UseMySQL(connectionString, options =>
							options.MigrationsAssembly(migrationsAssembly));
					};
                })
                //.AddTestUsers(Config.GetUsers())
                .AddDeveloperSigningCredential();
            // 2���û���ص�����
            services.AddDbContext<IdentityServerUserDbContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("CyfMSSQLConnection"));
                options.UseMySQL(Configuration.GetConnectionString("CyfMYSQLConnection"));
            });
            // 1.1 ����û�
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                // 1.2 ���븴�Ӷ�����
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<IdentityServerUserDbContext>()
            .AddDefaultTokenProviders();
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitializeDatabase(app);
            //InitializeUserDatabase(app);
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // 1����config�����ݴ洢����
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

        // 2�����û������ݴ洢����
        private void InitializeUserDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdentityServerUserDbContext>();
                context.Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var idnetityUser = userManager.FindByNameAsync("tony1").Result;
                if (idnetityUser == null)
                {
                    idnetityUser = new IdentityUser
                    {
                        UserName = "tony1",
                        Email = "tony1@email.com"
                    };
                    var result = userManager.CreateAsync(idnetityUser, "123456").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    result = userManager.AddClaimsAsync(idnetityUser, new Claim[] {
                        new Claim(JwtClaimTypes.Name, "tony1"),
                        new Claim(JwtClaimTypes.GivenName, "tony1"),
                        new Claim(JwtClaimTypes.FamilyName, "tony1"),
                        new Claim(JwtClaimTypes.Email, "tony1@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://tony.com")
                    }).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
