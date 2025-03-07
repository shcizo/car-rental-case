using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using CarRental.SharedKernel;
using MediatR;

namespace CarRental.UseCase.Car.Create;

public record Create(string RegistrationNr, int Odometer, CarType Type) : IRequest<Result<int>>;

public class CreateHandler(ICarRepository carRepository) : IRequestHandler<Create, Result<int>>
{
    public async Task<Result<int>> Handle(Create request, CancellationToken cancellationToken)
    {
        var existingCar = await carRepository.GetCarByRegNr(request.RegistrationNr, cancellationToken);
        if (existingCar is not null) return Result<int>.Invalid("Car already exists");

        var car = new Core.Models.Car
        {
            RegistrationNumber = request.RegistrationNr,
            Odometer = request.Odometer,
            Type = request.Type,
        };
        
        await carRepository.CreateCar(car);
        
        return Result<int>.Success(car.Id);
    }
}