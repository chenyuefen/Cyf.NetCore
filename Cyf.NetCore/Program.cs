using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Cyf.Core.Utility.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cyf.NetCore
{
    /// <summary>
    /// *********** �ܵ�����ģ��--�м�� ***********
    /// Startup--Config����ȥָ����Http����ܵ�
    /// ��νhttp����Ĺܵ��أ�
    /// ���Ƕ�Http�����һ�����Ĵ������
    /// ���Ǹ���һ��HttpContext��Ȼ��һ�����Ĵ������յĵõ����
    /// 
    /// 
    /// *********** �¾ɹܵ�ģ�͵����� ***********
    /// Asp.Net����ܵ��� �������ջ���һ�������HttpHandler����(page/ashx/mvchttphandler--action)
    ///                   ���ǻ��ж�����裬����װ���¼�--����ע�������չ--IHttpModule--�ṩ�˷ǳ��������չ��
    /// ��һ��ȱ�ݣ�̫������¶���--һ��http�����������IHttpHandler--cookie Session  Cache NeginRequest endrequest maprequesthandler ��Ȩ
    /// ----��Щ��һ���ǵ���---����д����---Ĭ����Ϊ��Щ�����Ǳ����---����ܵ����˼���й�---.Net���ż򵥾�ͨ��---��Ϊ��ܴ��������
    /// ȫ��Ͱʽ�������һ�¿ؼ���д�����ݿ⣬һ����Ŀ�ͳ�����---���Ծ�ͨҲ��----ҲҪ�������ۣ����ǰ����Ƚ��أ�������װǰ��
    /// ---.NetCore��һ��ȫ�µ�ƽ̨���Ѿ�������ǰ����---��Ƹ�׷���������׷�������---û��ȫ��Ͱ        
    /// 
    /// Asp.NetCoreȫ�µ�����ܵ���
    /// Ĭ��������ܵ�ֻ��һ��404
    /// Ȼ���������������Ĵ���(UseEndPoint)---�������ǰhandler��ֻ����ҵ����
    /// �����ľ����м��middleware
    /// 
    /// 
    /// *********** ����Autofac��AOP,IOC����� ***********
    /// a nuget--���Բο������������autofac��أ�3����⣩
    /// b [Program][CreateHostBuilder]������UseServiceProviderFactory��չ
    /// c [Startup]����ConfigureContainer(ContainerBuilder containerBuilder)����
    /// 
    /// 
    /// *********** Filter��������Action,Result,Exception, Resource ***********
    ///  Asp.Net Core:Action&Result&Exception
    ///  
    /// ȫ��ע������ConfigureServices[addmvc][addfilter]��ɵ�
    ///      
    /// ȫ�� ������  Action �ֱ�ע�ᣬִ��˳����
    /// ȫ��--������--Action--Actionִ�й���--Action--������--ȫ��
    /// 
    /// Result����Action֮��
    /// 
    /// �����ڱ����ʱ�򣬻�����metadata������IL  ������ȷ���ģ�������DI�ṩ
    /// ���Filter��Ҫע�룬��ô����ֱ�ӱ�ǵ�
    /// 1 ServiceFilter   ����Ҫ��ConfigServiceָ��һ��
    /// 2 TypeFilter
    /// 3 ȫ��
    /// 4 IFilterFactory
    /// order С����ִ��  Ĭ����0
    /// 
    /// AuthorityFilter��һ˳��
    /// ResourceFilter�ڶ�˳��
    /// ���úڰ塿��ɺ󣬲Ż�ȥʵ����������������
    /// Resource���ʺ������棬�����������ʱ����ɻ���
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((context, loggingBuilder) =>
                {
                    Log4Extention.InitLog4(loggingBuilder);
                })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
