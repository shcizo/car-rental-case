using CarRental.Core.Models;

namespace CarRental.Core.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetBookingByBookingNumber(string bookingNumber, CancellationToken cancellationToken);
    Task<Booking?> GetActiveBookingByCarId(int carId, CancellationToken cancellationToken);
    Task UpdateBooking(Booking booking);
    Task CreateBooking(Booking booking);
}