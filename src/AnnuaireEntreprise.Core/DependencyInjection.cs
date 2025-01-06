// Author: Salar
// Created: 06/01/2024
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AnnuaireEntreprise.Core;


namespace STIVE.core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IServiceService, ServiceService>();
            return services;
        }
    }
}