using CarRental.Api.Extensions;
using CarRental.Api.Models;
using CarRental.UseCase.Booking.Create;
using CarRental.UseCase.Booking.Get;
using CarRental.UseCase.Booking.Init;
using CarRental.UseCase.Booking.Return;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Endpoints;

public static class BookingEndpoints
{
    
    /// <summary>
    /// Perform some rudimentry validation of a booking making
    /// sure all values are actually sent in
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    private static bool IsValidBooking(CreateBooking booking)
    {
        return (string.IsNullOrWhiteSpace(booking.CustomerIdentification) ||
                booking.CarId < 0 ||
               DateTimeOffset.MinValue == booking.Date) is false;

    }

    private static bool IsValidReturn(ReturnBooking booking)
    {
        return (DateTimeOffset.MinValue == booking.Date) is false;
    }
    public static void MapBookingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/bookings");

        group.MapGet("/{bookingNr}", async (string bookingNr, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetQuery(bookingNr));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/init", async (IMediator mediator) =>
        {
            var bookingResult = await mediator.Send(new InitCommand());
            return bookingResult.ToMinimalApiResult();
        });

        group.MapPut("/{bookingNr}", async (string bookingNr, [FromBody] CreateBooking createBooking, [FromServices] IMediator mediator) =>
        {
            if (!IsValidBooking(createBooking))
                return Results.BadRequest(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Values are invalid.",
                });
            
            var result = await mediator.Send(new CreateCommand(bookingNr, createBooking.CarId, createBooking.CustomerIdentification, createBooking.Date));

            return result.ToMinimalApiResult();
        });

        group.MapPost("/{bookingNr}/return",
            async (string bookingNr, [FromBody] ReturnBooking returnBooking, [FromServices] IMediator mediator) =>
            {
                if (!IsValidReturn(returnBooking))  
                    return Results.BadRequest("Values are invalid.");
                
                var result = await mediator.Send(new ReturnCommand(bookingNr, returnBooking.Odometer, returnBooking.Date));

                return result.ToMinimalApiResult();
            });


    }
    
}