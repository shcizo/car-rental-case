
using Microsoft.EntityFrameworkCore;

namespace CarRental.Core.Models;

public class Setting : BaseEntity
{
    [Precision(18, 2)]
    public decimal BaseDayFee { get; set; }
    
    [Precision(18, 2)]
    public decimal BaseKmFee { get; set; }
    public string DealerShipName { get; set; } = string.Empty;
    public string DealerShortName { get; set; } = string.Empty;
}