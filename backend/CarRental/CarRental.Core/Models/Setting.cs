namespace CarRental.Core.Models;

public class Setting : BaseEntity
{
    public decimal BaseDayFee { get; set; }
    public decimal BaseKmFee { get; set; }
}