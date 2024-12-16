using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReservationPlatform.Common;

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
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CommentMaxLength)]
        [MinLength(ValidationConstants.CommentMinLength)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
