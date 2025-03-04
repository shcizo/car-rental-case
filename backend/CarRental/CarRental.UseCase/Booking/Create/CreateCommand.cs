using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using MediatR;

namespace CarRental.UseCase.Booking.Create;

public record CreateCommand(string BookingNumber, string RegistrationNumber, string CustomerIndentification, CarType Type, DateTimeOffset Date, int Odometer) : IRequest;

public class CreateCommandHandler(IBookingRepository bookingRepository, ICarRepository carRepository) : IRequestHandler<CreateCommand>
{
    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetCarByRegNr(request.RegistrationNumber, cancellationToken);

        if (car == null)
        {
            car = new Car { RegistrationNumber = request.RegistrationNumber, Odemeter = request.Odometer, Type = request.Type };
            await carRepository.CreateCar(car);
        }

        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNumber, cancellationToken);

        if (booking == null)
            return;

        booking.Car = car;
        booking.CusomterIdentifcation = request.CustomerIndentification;
        booking.HandOutDateUtc = request.Date;

        await bookingRepository.UpdateBooking(booking);
    }
}