using CarRental.Core.Interfaces;
using CarRental.UseCase.Services;
using MediatR;

namespace CarRental.UseCase.Booking.Return;

public record ReturnCommand(string BookingNr, int Odometer, DateTimeOffset Date) : IRequest;

public class ReturnCommandHandler(IBookingRepository bookingRepository, ICarRepository carRepository, ISettingsRepository settingsRepository) : IRequestHandler<ReturnCommand>
{
    public async Task Handle(ReturnCommand request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNr, cancellationToken);

        var settings = await settingsRepository.GetSettings(cancellationToken);

        if (booking is null || settings is null) return;

        booking.ReturnOdeMeter = request.Odometer;
        booking.ReturnDateUtc = request.Date;
        
        var oldOdometer = booking.Car!.Odemeter;
        var newOdometer = request.Odometer;
        var rentalDuration = booking.ReturnDateUtc!.Value - booking.HandOutDateUtc!.Value;
        booking.CalculatedPrice = PricingService.CalculatePrice(newOdometer - oldOdometer, booking.Car.Type, settings, rentalDuration);

        await bookingRepository.UpdateBooking(booking);
        await carRepository.UpdateCar(booking.Car);
        
    }
}