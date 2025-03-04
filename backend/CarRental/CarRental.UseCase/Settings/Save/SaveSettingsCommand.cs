using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using MediatR;

namespace CarRental.UseCase.Settings;

public record SaveSettingsCommand(string DealerShipName, string DealerShipShortName, decimal BaseDistanceFee, decimal BaseDayFee) : IRequest;

public class SaveSettingsHandler(ISettingsRepository settingsRepository) : IRequestHandler<SaveSettingsCommand>
{
    public async Task Handle(SaveSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await settingsRepository.GetSettings(cancellationToken) ?? new Setting();
        settings.DealerShipName = request.DealerShipName;
        settings.DealerShortName = request.DealerShipShortName;
        settings.BaseDayFee = request.BaseDayFee;
        settings.BaseKmFee = request.BaseDistanceFee;
        
        await settingsRepository.UpdateSettings(settings);
    }
}
