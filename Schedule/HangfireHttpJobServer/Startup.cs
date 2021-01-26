using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.HttpJob;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using TM.Infrastructure.Configs;

namespace HangfireHttpJobServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add the processing server as IHostedService
            services.AddHangfire(Configuration);
        }


        #region Hangfire Config
        private void Configuration(IGlobalConfiguration globalConfiguration)
        {
            globalConfiguration.UseStorage(new MySqlStorage(
                        ConfigHelper.Configuration["MallMySqlConnection"].ToString(),
                        new MySqlStorageOptions
                        {
                            TransactionIsolationLevel = IsolationLevel.ReadCommitted, // 事务隔离级别。默认是读取已提交。
                            QueuePollInterval = TimeSpan.FromSeconds(15),             //- 作业队列轮询间隔。默认值为15秒。
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- 聚合计数器的间隔。默认为5分钟。
                            PrepareSchemaIfNecessary = true,                          //- 如果设置为true，则创建数据库表。默认是true。
                            DashboardJobListLimit = 50000,                            //- 仪表板作业列表限制。默认值为50000。
                            TransactionTimeout = TimeSpan.FromMinutes(1),             //- 交易超时。默认为1分钟。
                            TablePrefix = "Hangfire"                                  //- 数据库中表的前缀。默认为none
                        }))
                .UseConsole(new ConsoleOptions()
                {
                    BackgroundColor = "#000079"
                })
                .UseHangfireHttpJob(new HangfireHttpJobOptions
                {
                    DefaultBackGroundJobQueueName = "recurring"
                });
        }
        #endregion


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logging)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            #region NLOG
            NLog.LogManager.LoadConfiguration("NLog.Config");
            logging.AddNLog();
            #endregion

            #region UI Language
            //显示中文
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            //显示英文
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            #endregion


            //启动hangfire服务
            #region 启动hangfire服务
            var queues = new List<string> { "default", "apis", "recurring" };
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                ServerTimeout = TimeSpan.FromMinutes(4),
                SchedulePollingInterval = TimeSpan.FromSeconds(15),//秒级任务需要配置短点，一般任务可以配置默认时间，默认15秒
                ShutdownTimeout = TimeSpan.FromMinutes(30),//超时时间
                Queues = queues.ToArray(),//队列
                WorkerCount = Math.Max(Environment.ProcessorCount, 40)//工作线程数，当前允许的最大线程，默认20
            });
            #endregion


            //使用hangfire面板
            #region 使用hangfire面板
            var hangfireStartUpPath = "/job";
            app.UseHangfireDashboard(hangfireStartUpPath, new DashboardOptions
            {
                AppPath = "#",
                DisplayStorageConnectionString = false,
                IsReadOnlyFunc = Context => false,
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users = new []
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "admin",
                            PasswordClear =  "888888"
                        }
                    }

                }) }
            });

            var hangfireReadOnlyPath = "/job-read";
            //只读面板，只能读取不能操作
            app.UseHangfireDashboard(hangfireReadOnlyPath, new DashboardOptions
            {
                IgnoreAntiforgeryToken = true,//这里一定要写true 不然用client库写代码添加webjob会出错
                AppPath = hangfireStartUpPath,//返回时跳转的地址
                DisplayStorageConnectionString = false,//是否显示数据库连接信息
                IsReadOnlyFunc = Context => true
            });
            #endregion

        }
    }
}
