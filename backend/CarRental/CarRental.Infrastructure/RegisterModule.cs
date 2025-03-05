using CarRental.Infrastructure.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure;

public static class RegisterModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
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