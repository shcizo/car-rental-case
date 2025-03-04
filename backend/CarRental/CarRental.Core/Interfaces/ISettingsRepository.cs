using CarRental.Core.Models;

namespace CarRental.Core.Interfaces;

public interface ISettingsRepository
{
    Task<Setting?> GetSettings(CancellationToken cancellationToken);
}