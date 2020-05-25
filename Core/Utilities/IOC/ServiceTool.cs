using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IOC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProviders { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProviders = services.BuildServiceProvider();
            return services;
        }
    }
}
