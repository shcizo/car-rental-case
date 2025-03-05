namespace CarRental.UseCase.Settings.Dto;

public class SettingDto
{
    public required string DealerShipName { get; set; }
    public required string DealerShipShortName { get; set; }
    public decimal BaseDistanceFee { get; set; }
    public decimal BaseDayFee { get; set; }
}