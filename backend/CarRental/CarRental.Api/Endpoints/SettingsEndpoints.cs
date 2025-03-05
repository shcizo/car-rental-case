using CarRental.Api.Models;
using CarRental.UseCase.Settings;
using CarRental.UseCase.Settings.Get;
using MediatR;

namespace CarRental.Api.Endpoints;

public static class SettingsEndpoints
{
    public static void MapSettingsEndpoints(this IEndpointRouteBuilder routes)
    {
        var groupRoutes = routes.MapGroup("/settings");

        groupRoutes.MapPost("", async (SaveSettingsModel model, IMediator mediator) =>
        {
            await mediator.Send(new SaveSettingsCommand(model.DealerShipName, model.DealerShipShortName, model.BaseDistanceFee, model.BaseDayFee));
            return Results.Ok();
        });

        groupRoutes.MapGet("", async (IMediator mediator) =>
        {
            var result =await mediator.Send(new GetQuery());
            
            return Results.Ok(result);
        });
    }
}
