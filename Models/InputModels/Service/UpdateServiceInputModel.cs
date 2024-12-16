using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class UpdateServiceInputModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; } 

    public List<SelectListItem> Categories { get; set; }
    public string ImageUrl { get; set; }
    public string Availability { get; set; }
}
