using System.ComponentModel.DataAnnotations;

namespace ServicesPlatform.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
