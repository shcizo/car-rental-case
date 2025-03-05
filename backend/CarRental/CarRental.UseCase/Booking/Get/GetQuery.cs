using CarRental.Core.Interfaces;
using CarRental.SharedKernel;
using CarRental.UseCase.Booking.Dto;
using MediatR;

namespace CarRental.UseCase.Booking.Get;

public record GetQuery(string BookingNumber) : IRequest<Result<BookingDto>>;

public class GetQueryHandler(IBookingRepository bookingRepository) : IRequestHandler<GetQuery, Result<BookingDto>>
{
    public async Task<Result<BookingDto>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetBookingByBookingNumber(request.BookingNumber, cancellationToken);

        if (booking == null) return Result<BookingDto>.NotFound("Booking not found");

        return Result<BookingDto>.Success(new BookingDto
        {
            HandOutDateUtc = booking.HandOutDateUtc,
            BookingNumber = booking.BookingNumber,
            ReturnDateUtc = booking.ReturnDateUtc,
            ReturnOdeMeter = booking.ReturnOdeMeter,
            CalculatedPrice = booking.CalculatedPrice,
            CusomterIdentifcation = booking.CusomterIdentifcation,
            RegistrationNumber = booking.Car?.RegistrationNumber,
            Odometer = booking.Car?.Odemeter ?? 0,
        });
    }
}