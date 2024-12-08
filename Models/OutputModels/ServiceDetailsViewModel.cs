using ServicesPlatform.Data.Models;
using System.Collections.Generic;

namespace ReservationPlatform.OutputModels
{
    public class ServiceDetailsViewModel
    {
        public Service Service { get; set; }
        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public Review NewReview { get; set; }
    }
}
