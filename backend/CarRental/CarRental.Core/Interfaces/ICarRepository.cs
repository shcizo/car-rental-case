using CarRental.Core.Models;

namespace CarRental.Core.Interfaces;

public interface ICarRepository
{
    Task<Car?> GetCarByRegNr(string registrationNumber, CancellationToken cancellationToken);
    Task CreateCar(Car car);
    Task UpdateCar(Car car);
}