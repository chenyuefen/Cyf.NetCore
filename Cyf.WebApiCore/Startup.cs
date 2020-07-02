using System;
using System.Collections.Generic;
using System.Linq;
using Cyf.Swagger;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Cyf.JWTAuthentication;
using Cyf.Cors;
using Cyf.WebApiCore.Extensions;
using Helpers;

namespace Cyf.WebApiCore
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
            //AddTransient瞬时模式：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例
            //AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
            //AddSingleton单例模式：每次都获取同一个实例

            var jwtOptions = new JWTOptions();
            Configuration.Bind("JwtOptions", jwtOptions);
            services.AddJwtBearerExt(jwtOptions);

            services.AddCorsExt();
            #region 注册Swagger服务 
            services.AddApiVersionExt();
            services.AddSwaggerGenExt();
            #endregion
            services.AddControllers()
                .AddNewtonsoftJsonExt();

            services.AddEasyCachingExt(Configuration);
            //services.AddCsRedisByEasyCaching();
            services.AddCsRedisHelper(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region 使用Swagger中间件
            app.UseSwaggerExt(apiVersionDescriptionProvider);
            #endregion
            app.UseCorsExt();
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
