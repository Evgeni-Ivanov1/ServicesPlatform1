using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesPlatform.Models.InputModels.Service
{
    public class UpdateServiceInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }
        public string Availability { get; set; }

        public string CreatorId { get; set; } 

        public List<SelectListItem> Categories { get; set; }
    }
}
