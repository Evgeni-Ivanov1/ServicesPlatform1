using System;

namespace ReservationPlatform.Models
{
    public class AdminLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set;}
        public DateTime PerformedOn { get; set; }
    }
}
