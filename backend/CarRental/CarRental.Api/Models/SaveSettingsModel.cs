namespace CarRental.Api.Models;

public class SaveSettingsModel
{
    public string DealerShipName { get; set; } = string.Empty;
    public string DealerShipShortName { get; set; } = string.Empty;
    public decimal BaseDayFee { get; set; }
    public decimal BaseDistanceFee { get; set; }
}