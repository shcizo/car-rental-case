using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Sql.Repositories;

public class SettingsRepository(CarRentalContext context) : ISettingsRepository
{
    public async Task<Setting?> GetSettings(CancellationToken cancellationToken)
    {
        return await context.Settings.FirstOrDefaultAsync(cancellationToken);
    }
}