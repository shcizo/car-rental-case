using System.ComponentModel.DataAnnotations;

namespace CarRental.Core.Models;

public class Car : BaseEntity
{
    public int Odemeter { get; set; }
    public CarType Type { get; set; }
    [MaxLength(6)]
    public required string RegistrationNumber { get; set; }
}

public enum CarType
{
    None = 0,
    SmallCar = 1,
    WagonCar = 2,
    TruckCar = 3,
}