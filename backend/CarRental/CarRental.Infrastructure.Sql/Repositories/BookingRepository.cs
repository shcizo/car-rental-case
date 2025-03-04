using CarRental.Core.Interfaces;
using CarRental.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Sql.Repositories;

public class BookingRepository(CarRentalContext context) : IBookingRepository
{
    public async Task<Booking?> GetBookingByBookingNumber(string bookingNumber, CancellationToken cancellationToken)
    {
        return await context.Bookings.Include(booking => booking.Car).FirstOrDefaultAsync(x => x.BookingNumber == bookingNumber, cancellationToken);
    }

    public async Task UpdateBooking(Booking booking)
    {
        context.Entry(booking).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task CreateBooking(Booking booking)
    {
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();
    }
}