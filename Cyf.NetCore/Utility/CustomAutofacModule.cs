using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Cyf.NetCore.Interface;
using Cyf.NetCore.Servcie;
using Cyf.NetCore2.MVC6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cyf.NetCore.Utility
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();


            containerBuilder.Register(c => new CustomAutofacAop());//aop注册
            containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();
            containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();
            containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>();

            containerBuilder.RegisterType<A>().As<IA>().EnableInterfaceInterceptors();//允许aop扩展

            //containerBuilder.Register<FirstController>();

        }
    }

}
