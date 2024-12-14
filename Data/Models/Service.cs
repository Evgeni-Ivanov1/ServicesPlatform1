using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationPlatform.Common;

namespace ServicesPlatform.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ServiceNameMaxLength)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Availability { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
