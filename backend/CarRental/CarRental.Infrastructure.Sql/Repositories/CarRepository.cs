using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Sql.Repositories;

public class CarRepository(CarRentalContext context) : ICarRepository
{
    public async Task<Car?> GetCarByRegNr(string registrationNumber, CancellationToken cancellationToken)
    {
        return await context.Cars.FirstOrDefaultAsync(car => car.RegistrationNumber == registrationNumber, cancellationToken);
    }

    public async Task<Car?> GetCarById(int carId, CancellationToken cancellationToken)
    {
        return await context.Cars.FirstOrDefaultAsync(car => car.Id == carId, cancellationToken);
    }

    public async Task CreateCar(Car car)
    {
        context.Cars.Add(car);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCar(Car car)
    {
        await context.SaveChangesAsync();
    }
}