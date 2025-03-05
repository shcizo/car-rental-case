using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using CarRental.SharedKernel;
using CarRental.UseCase.Settings.Dto;
using MediatR;

namespace CarRental.UseCase.Settings.Get;

public record GetQuery : IRequest<Result<SettingDto>>;

public class GetQueryHandler(ISettingsRepository settingsRepository) : IRequestHandler<GetQuery, Result<SettingDto>>
{
    public async Task<Result<SettingDto>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var setting = await settingsRepository.GetSettings(cancellationToken) ?? new Setting();

        return Result<SettingDto>.Success(new SettingDto()
        {
            DealerShipName = setting.DealerShipName,
            DealerShipShortName = setting.DealerShortName,
            BaseDayFee = setting.BaseDayFee,
            BaseDistanceFee = setting.BaseKmFee,
        });
    }
}