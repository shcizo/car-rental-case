using CarRental.Api.Models;
using CarRental.Core.Models;
using CarRental.UseCase.Booking.Create;
using CarRental.UseCase.Booking.Get;
using CarRental.UseCase.Booking.Init;
using CarRental.UseCase.Booking.Return;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Endpoints;

public static class BookingEndpoints
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/bookings");

        group.MapGet("/{bookingNr}", async (string bookingNr, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetQuery(bookingNr));
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        group.MapPost("/init", async (IMediator mediator) =>
        {
            var bookingDto = await mediator.Send(new InitCommand());
            return Results.Ok(bookingDto);
        });

        group.MapPut("/{bookingNr}", async (string bookingNr, [FromBody] CreateBooking createBooking, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new CreateCommand(bookingNr, createBooking.RegistrationNumber, createBooking.CustomerIdentification,
                Enum.Parse<CarType>(createBooking.Type), createBooking.Date, createBooking.Odometer));

            return Results.Ok();
        });

        group.MapPost("/{bookingNr}/return",
            async (string bookingNr, [FromBody] ReturnBooking returnBooking, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new ReturnCommand(bookingNr, returnBooking.Odometer, returnBooking.Date));

                return Results.Ok();
            });
    }
}