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

        public string OwnerId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ServiceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ServiceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

      
        public string Availability { get; set; }

     
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
