using System.ComponentModel.DataAnnotations;

namespace ServicesPlatform.Models.InputModels.Review
{
    public class AddReviewInputModel
    {
        public int ServiceId { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
