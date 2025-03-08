using CarRental.Core.Interfaces;
using CarRental.SharedKernel;
using MediatR;

namespace CarRental.UseCase.Booking.Create;

public record CreateCommand(string BookingNumber, int CarId, string CustomerIndentification, DateTimeOffset Date) : IRequest<Result>;

public class CreateCommandHandler(IBookingRepository bookingRepository, ICarRepository carRepository) : IRequestHandler<CreateCommand, Result>
{
    public async Task<Result> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarById(request.CarId, cancellationToken);

        if (car == null) return Result.NotFound("No car was found");
        
        var existingBooking = await bookingRepository.GetActiveBookingByCarId(car.Id, cancellationToken);
        
        if (existingBooking is not null) return Result.Invalid("There is already an existing booking for car");

        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNumber, cancellationToken);

        if (booking == null) return Result.NotFound("Booking not found");
        
        booking.Car = car;
        booking.CusomterIdentifcation = request.CustomerIndentification;
        booking.HandOutDateUtc = request.Date;
        booking.StartOdometer = car.Odometer;

        await bookingRepository.UpdateBooking(booking);
        
        return Result.Success();
    }
}