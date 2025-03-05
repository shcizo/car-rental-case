using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using CarRental.SharedKernel;
using CarRental.UseCase.Booking.Dto;
using MediatR;

namespace CarRental.UseCase.Booking.Init;

public record InitCommand : IRequest<Result<BookingDto>>;

public class InitCommandHandler(IBookingRepository bookingRepository, ISettingsRepository settingsRepository) : IRequestHandler<InitCommand, Result<BookingDto>>
{
    public async Task<Result<BookingDto>> Handle(InitCommand request, CancellationToken cancellationToken)
    {
        var setting = await settingsRepository.GetSettings(cancellationToken) ?? new Setting();
        
        var booking = new Core.Models.Booking();
        await bookingRepository.CreateBooking(booking);

        var paddedId = booking.Id.ToString();

        // Pad the id because it looks good
        if (paddedId.Length < 5)
        {
            paddedId = paddedId.PadLeft(5, '0');
        }
        
        booking.BookingNumber = $"{setting.DealerShortName}-{paddedId}";
        await bookingRepository.UpdateBooking(booking);

        return Result<BookingDto>.Success(new BookingDto()
        {
            BookingNumber = booking.BookingNumber,
        });
    }
}
