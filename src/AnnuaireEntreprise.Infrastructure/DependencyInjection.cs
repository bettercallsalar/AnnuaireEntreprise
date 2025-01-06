// Author: Salar
// Created: 06/01/2024
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace AnnuaireEntreprise.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddSingleton(configuration); // Charger la configuration dans DI
                services.AddSingleton<MySqlConnector>(); // Ajouter MySqlConnector
                services.AddScoped<IServiceRepository, ServiceRepository>();
                services.AddScoped<ISiteRepository, SiteRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                return services;
        }


        // public static IConfiguration AddConfiguration()
        // {
        //         var currentDirectory = Directory.GetCurrentDirectory();

        //         var infrastructureConfigPath = Path.Combine(currentDirectory, "../AnnuaireEntreprise.Infrastructure/appsettings.json");

        //         return new ConfigurationBuilder()
        //             .SetBasePath(currentDirectory)
        //             .AddJsonFile(infrastructureConfigPath, optional: false, reloadOnChange: true)
        //             .Build();
        // }

}
