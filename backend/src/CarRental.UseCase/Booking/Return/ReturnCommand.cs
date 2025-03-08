using CarRental.Core.Interfaces;
using CarRental.SharedKernel;
using CarRental.UseCase.Services;
using MediatR;

namespace CarRental.UseCase.Booking.Return;

public record ReturnCommand(string BookingNr, int Odometer, DateTimeOffset Date) : IRequest<Result>;

public class ReturnCommandHandler(IBookingRepository bookingRepository, ICarRepository carRepository, ISettingsRepository settingsRepository) : IRequestHandler<ReturnCommand, Result>
{
    public async Task<Result> Handle(ReturnCommand request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNr, cancellationToken);

        if (booking == null) return Result.NotFound("Booking not found");

        var settings = await settingsRepository.GetSettings(cancellationToken);

        if (settings is null) return Result.Failure("There is not settings for this dealership");

        // This booking has already been returned
        if (booking.ReturnDateUtc.HasValue) return Result.Invalid("Booking has already been returned");

        // Date is incorrect
        if (request.Date <= booking.HandOutDateUtc) return Result.Invalid("Return date cannot be before hand out");

        booking.ReturnOdeMeter = request.Odometer;
        booking.ReturnDateUtc = request.Date;

        var oldOdometer = booking.Car!.Odometer;
        var newOdometer = request.Odometer;
        booking.Car!.Odometer = newOdometer;
        var rentalDuration = booking.ReturnDateUtc!.Value - booking.HandOutDateUtc!.Value;
        booking.CalculatedPrice = PricingService.CalculatePrice(newOdometer - oldOdometer, booking.Car.Type, settings, rentalDuration);

        await bookingRepository.UpdateBooking(booking);
        await carRepository.UpdateCar(booking.Car);

        return Result.Success();
    }
}