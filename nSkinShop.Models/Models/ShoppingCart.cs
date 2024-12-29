using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nSkinShop.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }
    [Range(1, 1500, ErrorMessage = "Please enter a value between 1 and 1500")]
    public int Quantity { get; set; }
    
    public string? ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser? ApplicationUser { get; set; }
    
    public string? SessionId { get; set; }
    
    public int? PromoCodeId { get; set; }
    
    [ForeignKey("PromoCodeId")]
    [ValidateNever]
    public PromoCode? PromoCode { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; } 
}