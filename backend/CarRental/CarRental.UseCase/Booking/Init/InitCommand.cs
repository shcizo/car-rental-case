using CarRental.Core.Interfaces;
using CarRental.UseCase.Booking.Dto;
using MediatR;

namespace CarRental.UseCase.Booking.Init;

public record InitCommand : IRequest<BookingDto>;

public class InitCommandHandler(IBookingRepository bookingRepository) : IRequestHandler<InitCommand, BookingDto>
{
    public async Task<BookingDto> Handle(InitCommand request, CancellationToken cancellationToken)
    {
        var booking = new Core.Models.Booking();
        await bookingRepository.CreateBooking(booking);
        
        var paddedId = booking.Id.ToString("00000000");
        booking.BookingNumber = $"CR-{paddedId}";
        await bookingRepository.UpdateBooking(booking);

        return new BookingDto()
        {
            BookingNumber = booking.BookingNumber,
        };
    }
}
