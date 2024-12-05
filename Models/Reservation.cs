using System;

namespace ReservationPlatform.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } 
        public string Username { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal Price { get; set; }
    }
}
