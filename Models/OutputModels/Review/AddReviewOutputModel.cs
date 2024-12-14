using System;

namespace ServicesPlatform.Models.OutputModels.Review
{
    public class AddReviewOutputModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
