using CarRental.Core.Interfaces;
using CarRental.SharedKernel;
using CarRental.UseCase.Car.Dto;
using MediatR;

namespace CarRental.UseCase.Car.Get;

public record GetQuery(string RegNr) : IRequest<Result<CarDto>>;

public class GetQueryHandler(ICarRepository carRepository) : IRequestHandler<GetQuery, Result<CarDto>>
{
    public async Task<Result<CarDto>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarByRegNr(request.RegNr, cancellationToken);
        if (car is null) return Result<CarDto>.NotFound("No car was found");

        return Result<CarDto>.Success(new CarDto
        {
            Id = car.Id,
            RegistrationNumber = car.RegistrationNumber,
            Odemeter = car.Odometer, Type = car.Type,
        });
    }
}