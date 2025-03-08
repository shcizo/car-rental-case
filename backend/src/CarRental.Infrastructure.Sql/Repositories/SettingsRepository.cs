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

    public async Task UpdateSettings(Setting settings)
    {
        if (context.Entry(settings).State == EntityState.Detached)
        {
            context.Settings.Add(settings);
            await context.SaveChangesAsync();
            return;
        }
        
        context.Entry(settings).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}