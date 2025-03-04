namespace CarRental.Api.Models;

public class CreateBooking
{
    public string RegistrationNumber { get; set; }

    public string CustomerIdentification { get; set; }

    public string Type { get; set; }

    public DateTimeOffset Date { get; set; }

    public int Odometer { get; set; }
}