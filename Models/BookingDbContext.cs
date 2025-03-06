using Microsoft.EntityFrameworkCore;
using MinimalAPIprojektLektion240304.Models;

namespace MinimalAPIprojektLektion240304.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; } = default!;
    }
}
