using ReservationPlatform.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesPlatform.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryMaxlenght)]  
        public string Description { get; set; }
        public ICollection<Service> Services { get; set; } = new HashSet<Service>();
    }
}
