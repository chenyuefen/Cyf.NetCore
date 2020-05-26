using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Cyf.Core.Utility.Filters;
using Cyf.EF.MSSQL.Model;
using Cyf.EF.MSSQL.Model.Log;
using Cyf.EF.MYSQL.Model;
using Cyf.NetCore.Interface;
using Cyf.NetCore.Middlewares;
using Cyf.NetCore.Servcie;
using Cyf.NetCore.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cyf.NetCore
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
            services.AddDbContext<CyfMYSQLContext>(options => options.UseMySQL(Configuration.GetConnectionString("CyfMYSQLConnection")));
            services.AddDbContext<CyfMSSQLContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CyfMSSQLConnection"))
                                                                     .UseLoggerFactory(new CustomEFLoggerFactory()));
            #region 系统内置DI  

            //services.AddTransient<ITestServiceA, TestServiceA>();//瞬时生命周期 
            //services.AddSingleton<ITestServiceB, TestServiceB>();//单例：全容器都是一个
            //services.AddScoped<ITestServiceC, TestServiceC>();//请求单例：一个请求作用域是一个实例

            #endregion

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(CustomExceptionFilterAttribute));
                o.Filters.Add(typeof(CustomGlobalActionFilterAttribute));
            });//全局注册filter

            services.AddScoped<CustomActionFilterAttribute>();//告诉DI

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        /// <summary>
        /// autofac
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            #region Middleware—中间件
            //app.Run(c => c.Response.WriteAsync("Hello World!"));   
            ////任何请求来了，只是返回个hello world    终结式
            ////所谓Run终结式注册，其实只是一个扩展方法，最终还不是得调用Use方法，

            //IApplicationBuilder 应用程序的组装者
            //RequestDelegate:传递一个HttpContext，异步操作下，不返回；也就是一个处理动作
            // Use(Func<RequestDelegate, RequestDelegate> middleware) 委托，传入一个RequestDelegate，返回一个RequestDelegate
            //ApplicationBuilder里面有个容器IList<Func<RequestDelegate, RequestDelegate>> _components
            //Use就只是去容器里面添加个元素
            //最终会Build()一下， 如果没有任何注册，就直接404处理一切
            /*
             foreach (var component in _components.Reverse())//反转集合  每个委托拿出来
            {
                app = component.Invoke(app);
                //委托3-- 404作为参数调用，返回 委托3的内置动作--作为参数去调用委托(成为了委托2的参数)--循环下去---最终得到委托1的内置动作---请求来了HttpContext---
            }
             */
            //IApplicationBuilder build之后其实就是一个RequestDelegate，能对HttpContext加以处理
            //默认情况下，管道是空的，就是404；可以根据你的诉求，任意的配置执行，一切全部由开发者自由定制，框架只是提供了一个组装方式

            //Func<RequestDelegate, RequestDelegate> middleware = next =>
            //{
            //    return new RequestDelegate(async context =>
            //                    {
            //                        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            //                        await Task.CompletedTask;
            //                        await next.Invoke(context);//RequestDelegate--需要context返回Task
            //                        await context.Response.WriteAsync("<h3>This is Middleware1 end</h3>");
            //                    });
            //};
            //app.Use(middleware);

            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware1");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware1 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware1 end</h3>");
            //    });
            //});

            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware2");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware2 start</h3>");
            //        await next.Invoke(context);
            //        await context.Response.WriteAsync("<h3>This is Middleware2 end</h3>");
            //    });
            //});
            //app.Use(next =>
            //{
            //    System.Diagnostics.Debug.WriteLine("this is Middleware3");
            //    return new RequestDelegate(async context =>
            //    {
            //        await context.Response.WriteAsync("<h3>This is Middleware3 start</h3>");
            //        await next.Invoke(context);//注释掉，表示不再往下走
            //        await context.Response.WriteAsync("<h3>This is Middleware3 end</h3>");
            //    });
            //});

            ////1 Run 终结式  只是执行，没有去调用Next  
            ////一般作为终结点
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello World Run");
            //});
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello World Run Again");
            //});

            ////2 Use表示注册动作  不是终结点  
            ////执行next，就可以执行下一个中间件   如果不执行，就等于Run
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use1 <br/>");
            //    await next();
            //    await context.Response.WriteAsync("Hello World Use1 End <br/>");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use2 Again <br/>");
            //    await next();
            //});
            ////UseWhen可以对HttpContext检测后，增加处理环节;原来的流程还是正常执行的
            //app.UseWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //},
            //appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Hello World Use3 Again Again Again <br/>");
            //        await next();
            //    });
            //});

            //app.Use(async (context, next) =>//没有调用 next() 那就是终结点  跟Run一样
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //    //await next();
            //});

            ////Map：根据条件指定中间件  指向终结点，没有Next
            ////最好不要在中间件里面判断条件选择分支；而是一个中间件只做一件事儿，多件事儿就多个中间件
            //app.Map("/Test", MapTest);
            //app.Map("/Eleven", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"This is Advanced Eleven Site");
            //}));
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("Name");
            //    拒绝非chorme浏览器的请求
            //    多语言
            //    把ajax统一处理
            //}, MapTest);

            //Middleware类
            app.UseMiddleware<ErrorLoggerMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World Use3  Again Again <br/>");
            //});
            #endregion

            #region 配置文件的读取
            //xml path
            Console.WriteLine($"option1 = {this.Configuration["Option1"]}");
            Console.WriteLine($"option2 = {this.Configuration["option2"]}");
            Console.WriteLine(
                $"suboption1 = {this.Configuration["subsection:suboption1"]}");
            Console.WriteLine("Wizards:");
            Console.Write($"{this.Configuration["wizards:0:Name"]}, ");
            Console.WriteLine($"age {this.Configuration["wizards:0:Age"]}");
            Console.Write($"{this.Configuration["wizards:1:Name"]}, ");
            Console.WriteLine($"age {this.Configuration["wizards:1:Age"]}");
            #endregion

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "areas",
                  areaName:"System",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
