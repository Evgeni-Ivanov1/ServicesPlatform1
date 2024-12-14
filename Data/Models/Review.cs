using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesPlatform.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
