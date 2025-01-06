// Author: Salar
// Created: 06/01/2024
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AnnuaireEntreprise.Infrastructure;
namespace STIVE.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddSingleton(configuration);
                services.AddScoped<IServiceRepository, ServiceRepository>();
                services.AddScoped<ISiteRepository, SiteRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                return services;
        }


        public static IConfiguration AddConfiguration()
        {
                var currentDirectory = Directory.GetCurrentDirectory();

                var infrastructureConfigPath = Path.Combine(currentDirectory, "../STIVE.Infrastructure/appsettings.json");

                return new ConfigurationBuilder()
                    .SetBasePath(currentDirectory)
                    .AddJsonFile(infrastructureConfigPath, optional: false, reloadOnChange: true)
                    .Build();
        }

}
