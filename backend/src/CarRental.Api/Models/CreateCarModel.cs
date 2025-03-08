namespace CarRental.Api.Models;

public class CreateCarModel
{
    public string RegistrationNumber { get; set; } = string.Empty;

    public int Odometer { get; set; }
    public string Type { get; set; } = string.Empty;
}