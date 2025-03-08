using CarRental.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Sql;

public class CarRentalContext(DbContextOptions<CarRentalContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; } = default!;
    public DbSet<Booking> Bookings { get; set; } = default!;
    public DbSet<Setting> Settings { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=CarRental;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=facyvt04vdN868redwing;Trust Server Certificate=True;");
    }
}