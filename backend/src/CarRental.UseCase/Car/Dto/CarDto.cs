using CarRental.Core.Models;

namespace CarRental.UseCase.Car.Dto;

public class CarDto
{
    public int Id { get; set; }
    public int Odemeter { get; set; }
    public CarType Type { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
}