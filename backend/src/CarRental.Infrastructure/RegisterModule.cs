using CarRental.Infrastructure.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure;

public static class RegisterModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        /*
         * Very rudimentry plugin system for databases but you can basicly add any database/ORM in its own project
         * and reference core-project and implement the interfaces. Then add a keyword in appconfig and add a switch case below
         */ 
        var datbase = configuration["DataLayerConfiguration:Database"];
        switch (datbase)
        {
            case "MsSql":
                services.AddInfrastructureSql(configuration);
                break;
            default:
                throw new ApplicationException("Invalid database type. No database was specified.");
        }

        return services;
    }
}