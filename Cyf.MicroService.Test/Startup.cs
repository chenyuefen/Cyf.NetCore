using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cyf.MicroService.AggregateService.Services;
using Cyf.MicroService.Core.HttpClientConsul;
using Cyf.MicroService.Core.HttpClientPolly;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cyf.MicroService.Test
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
            // 1���Զ����쳣����(�û��洦��)
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent("ϵͳ����æ�����Ժ�����"),// ���ݣ��Զ�������
                StatusCode = HttpStatusCode.GatewayTimeout // 504
            };

            // 1.2 ��װ֮��ĵ���PollyHttpClient
            services.AddPollyHttpClient("consul", options =>
            {
                options.TimeoutTime = 1; // 1����ʱʱ��
                options.RetryCount = 3;// 2�����Դ���
                options.CircuitBreakerOpenFallCount = 2;// 3���۶�������(���ٴ�ʧ�ܿ���)
                options.CircuitBreakerDownTime = 100;// 4���۶�������ʱ��
                options.httpResponseMessage = fallbackResponse;// 5����������
            });

            // 1��ע��Consul����
            services.AddHttpClientConsul<ConsulHttpClient>();
            // 2��ע��team����
            services.AddSingleton<ITeamServiceClient, HttpTeamServiceClient>();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
