using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
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
    /// c ConfigureContainer(ContainerBuilder containerBuilder)
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
                })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
