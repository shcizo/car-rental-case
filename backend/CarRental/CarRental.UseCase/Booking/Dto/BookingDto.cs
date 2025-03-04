namespace CarRental.UseCase.Booking.Dto;

public class BookingDto
{
    public string BookingNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public DateTimeOffset? HandOutDateUtc { get; set; }
    public DateTimeOffset? ReturnDateUtc { get; set; }
    public int? ReturnOdeMeter { get; set; }
    public int Odometer { get; set; }
    public decimal? CalculatedPrice { get; set; }
    public string? CusomterIdentifcation { get; set; }
}