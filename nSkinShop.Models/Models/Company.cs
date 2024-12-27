using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nSkinShop.Models;

public class Company
{
    public int Id { get; set; }
    [Required] public string Name { get; set; }

    [DisplayName("Street Address")] public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [DisplayName("Postal Code")] public string? ZipCode { get; set; }
    [DisplayName("Phone Number")] public string? PhoneNumber { get; set; }
    
    
    
}