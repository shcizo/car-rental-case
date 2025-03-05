namespace CarRental.Api.Models;

public class CreateBooking
{
    public required string RegistrationNumber { get; set; }

    public required string CustomerIdentification { get; set; }

    public required string Type { get; set; }

    public DateTimeOffset Date { get; set; }

    public int Odometer { get; set; }
}