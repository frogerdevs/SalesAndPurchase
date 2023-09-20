﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalesAndPurchase.Server.Application.Extensions.Startup
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddDependencyApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddMediator();
            services.AddMediator(
                options =>
                {
                    options.ServiceLifetime = ServiceLifetime.Scoped;
                }
            );
            return services;

        }
    }
}