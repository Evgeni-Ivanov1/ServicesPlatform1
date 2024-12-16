using System;

namespace ServicesPlatform.Models.OutputModels.Reservation
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Username { get; set; }
        public decimal Price { get; set; }
        

    }
}
