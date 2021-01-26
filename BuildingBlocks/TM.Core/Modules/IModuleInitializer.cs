﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TM.Core.Modules
{
    public interface IModuleInitializer
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app, IHostEnvironment env);

    }
}
