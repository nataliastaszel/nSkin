using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nSkinShop.Models;

public class Product
{
    [Key] public int Id { get; set; }
    [Required] public string Brand { get; set; }
    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }
    [ValidateNever] public string? ImageUrl { get; set; }
    [Range(1.00, 10000.00)] [Required] public double Price { get; set; }

    [DisplayName("Price for 100ml")]
    [Range(1.00, 10000.00)]
    [Required]
    public double Price100ml { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
}