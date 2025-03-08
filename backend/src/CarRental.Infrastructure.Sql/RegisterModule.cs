using CarRental.Core.Interfaces;
using CarRental.Infrastructure.Sql.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure.Sql;

public static class RegisterModule
{
    public static IServiceCollection AddInfrastructureSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<CarRentalContext>(configuration.GetConnectionString("DefaultConnection"));
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        return services;
    }
}