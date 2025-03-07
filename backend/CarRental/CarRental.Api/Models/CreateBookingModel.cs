namespace CarRental.Api.Models;

public class CreateBooking
{
    public int CarId { get; set; }
    public required string CustomerIdentification { get; set; }
    public DateTimeOffset Date { get; set; }

}