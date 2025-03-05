using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using CarRental.UseCase.Settings.Dto;
using MediatR;

namespace CarRental.UseCase.Settings.Get;

public record GetQuery : IRequest<SettingDto>;

public class GetQueryHandler(ISettingsRepository settingsRepository) : IRequestHandler<GetQuery, SettingDto>
{
    public async Task<SettingDto> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var setting = await settingsRepository.GetSettings(cancellationToken) ?? new Setting();

        return new SettingDto()
        {
            DealerShipName = setting.DealerShipName,
            DealerShipShortName = setting.DealerShortName,
            BaseDayFee = setting.BaseDayFee,
            BaseDistanceFee = setting.BaseKmFee,
        };
    }
}