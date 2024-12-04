using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationPlatform.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
