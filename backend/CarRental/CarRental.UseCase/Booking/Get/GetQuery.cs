using CarRental.Core.Interfaces;
using CarRental.UseCase.Booking.Dto;
using MediatR;

namespace CarRental.UseCase.Booking.Get;

public record GetQuery(string BookingNumber) : IRequest<BookingDto?>;

public class GetQueryHandler(IBookingRepository bookingRepository) : IRequestHandler<GetQuery, BookingDto?>
{
    public async Task<BookingDto?> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNumber, cancellationToken);

        if (booking == null) return null;

        return new BookingDto
        {
            HandOutDateUtc = booking.HandOutDateUtc,
            ReturnDateUtc = booking.ReturnDateUtc,
            BookingNumber = booking.BookingNumber,
            ReturnOdeMeter = booking.ReturnOdeMeter,
            CalculatedPrice = booking.CalculatedPrice,
            CusomterIdentifcation = booking.CusomterIdentifcation,
            RegistrationNumber = booking.Car?.RegistrationNumber,
            Odometer = booking.Car?.Odemeter ?? 0,
        };
    }
}