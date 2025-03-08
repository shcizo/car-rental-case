namespace CarRental.Api.Models;

public class ReturnBooking
{
    public DateTimeOffset Date { get; set; }
    public int Odometer { get; set; }
}