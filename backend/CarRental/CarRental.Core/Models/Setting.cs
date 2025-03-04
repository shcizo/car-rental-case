namespace CarRental.Core.Models;

public class Setting : BaseEntity
{
    public decimal BaseDayFee { get; set; }
    public decimal BaseKmFee { get; set; }
    public string DealerShipName { get; set; } = string.Empty;
    public string DealerShortName { get; set; } = string.Empty;
}