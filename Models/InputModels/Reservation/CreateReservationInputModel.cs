using System;
using System.ComponentModel.DataAnnotations;

namespace ServicesPlatform.Models.InputModels.Reservation
{
    public class CreateReservationInputModel
    {
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
