using CarRental.Api.Extensions;
using CarRental.Api.Models;
using CarRental.Core.Models;
using CarRental.UseCase.Car.Create;
using CarRental.UseCase.Car.Get;
using MediatR;

namespace CarRental.Api.Endpoints;

public static class CarEndpoints
{
    public static void MapCarEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/cars");

        group.MapGet("{regNr}", async (string regNr, IMediator mediator) =>
        {
            var result = await  mediator.Send(new GetQuery(regNr));
            return result.ToMinimalApiResult();
        });

        group.MapPost("", async (CreateCarModel model, IMediator mediator) =>
        {
            var result = await mediator.Send(new Create(model.RegistrationNumber, model.Odometer, Enum.Parse<CarType>(model.Type)));
            return result.ToMinimalApiResult();
        });
    }
}