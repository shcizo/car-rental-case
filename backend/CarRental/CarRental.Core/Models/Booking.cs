using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Core.Models;

public class Booking : BaseEntity
{
    [MaxLength(50)]
    public string BookingNumber { get; set; } = string.Empty;

    public Car? Car { get; set; }
    public DateTimeOffset? HandOutDateUtc { get; set; }
    public DateTimeOffset? ReturnDateUtc { get; set; }

    public int? StartOdometer { get; set; }
    public int? ReturnOdeMeter { get; set; }
    
    [Precision(18, 2)]

    public decimal? CalculatedPrice { get; set; }

    [MaxLength(50)]
    public string? CusomterIdentifcation { get; set; }
}