using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MinimalAPIprojektLektion240304.Data;
using MinimalAPIprojektLektion240304.Models;
using System.Text.RegularExpressions;

namespace MinimalAPIprojektLektion240304.Endpoints
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this WebApplication app)
        {
            // POST /bookings: Skapa en ny bokning
            app.MapPost("/bookings", async (Booking booking, BookingDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(booking.CustomerName))
                {
                    return Results.BadRequest("Kundens namn är obligatoriskt.");
                }

                if (!Regex.IsMatch(booking.CustomerPhone, @"^\+?\d{7,15}$"))
                {
                    return Results.BadRequest("Ogiltigt telefonnummer.");
                }

                DateTime bookingDateTime = booking.Date.Date + booking.Time;
                if (bookingDateTime < DateTime.Now)
                {
                    return Results.BadRequest("Bokningstiden måste vara i framtiden.");
                }

                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return Results.Created($"/bookings/{booking.Id}", booking);
            });

            // GET /bookings: Hämta bokningar med paginering
            app.MapGet("/bookings", async (BookingDbContext db, int? page, int? pageSize) =>
            {
                int p = page ?? 1;
                int ps = pageSize ?? 10;
                var bookings = await db.Bookings
                    .Skip((p - 1) * ps)
                    .Take(ps)
                    .ToListAsync();
                return Results.Ok(bookings);
            });

            // GET /bookings/date/{date}: Hämta bokningar för ett specifikt datum
            app.MapGet("/bookings/date/{date}", async (DateTime date, BookingDbContext db) =>
            {
                var bookings = await db.Bookings
                    .Where(b => b.Date.Date == date.Date)
                    .ToListAsync();
                return Results.Ok(bookings);
            });

            // DELETE /bookings/{id}: Avboka en bokning
            app.MapDelete("/bookings/{id}", async (int id, BookingDbContext db) =>
            {
                var booking = await db.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return Results.NotFound();
                }

                db.Bookings.Remove(booking);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
