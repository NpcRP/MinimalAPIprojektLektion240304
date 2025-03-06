using System;

namespace MinimalAPIprojektLektion240304.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }       // Bokningsdatum (YYYY-MM-DD)
        public TimeSpan Time { get; set; }         // Bokningstid (HH:MM)
        public string HairdresserName { get; set; } = string.Empty; // Frisörens namn
        public string CustomerName { get; set; } = string.Empty;    // Kundens namn (obligatoriskt)
        public string CustomerPhone { get; set; } = string.Empty;   // Kundens telefonnummer
    }
}
