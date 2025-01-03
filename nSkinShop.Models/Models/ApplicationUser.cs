using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nSkinShop.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData] [Required] public string Name { get; set; }

    [PersonalData]
    [DisplayName("Street Address")]
    public string? StreetAddress { get; set; }

    [PersonalData] public string? City { get; set; }
    [PersonalData] public string? State { get; set; }

    [PersonalData]
    [DisplayName("Postal Code")]
    public string? PostalCode { get; set; }

    public int? CompanyId { get; set; }

    [ForeignKey("CompanyId")]
    [ValidateNever]
    [PersonalData]
    public Company Company { get; set; }
}