using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Models;

namespace nSkinShop.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Face Care Products", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Body Care Products", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Sun Care Products", DisplayOrder = 3 },
            new Category { Id = 4, Name = "Treatment & Specialty Products", DisplayOrder = 4 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1, Brand = "Drunk Elephant",
                Title = "Protini Polypeptide Cream",
                Description = "Moisturizing face cream",
                Price = 85.53,
                Price100ml = 116.27,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 2, Brand = "Clinique",
                Title = "Smart Clinical Repair Wrinkle Correcting Cream",
                Description = "Nourishing night cream",
                Price = 100.89,
                Price100ml = 205.77,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 3, Brand = "Sol de Janeiro",
                Title = "Brazilian Bum Bum Cream",
                Description = "Brazilian body cream",
                Price = 23.55,
                Price100ml = 31.34,
                CategoryId = 2,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 4, Brand = "Rituals",
                Title = "The Ritual of Ayurveda",
                Description = "Shower gel foam",
                Price = 13.55,
                Price100ml = 17.89,
                CategoryId = 2,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 5, Brand = "SuperGoop",
                Title = "Glowscreen",
                Description = "Sun protection filter SPF 30",
                Price = 24.54,
                Price100ml = 50.00,
                CategoryId = 3,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 6, 
                Brand = "Clarins", 
                Title = "Soothing After Sun Balm", 
                Description = "After sun care",
                Price = 34.46, 
                Price100ml = 76.55,
                CategoryId = 3,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 7, Brand = "Dr Irena Eris",
                Title = "Body Art Smoothing Body Scrub with Alabaster ",
                Description = "Smoothing peeling",
                Price = 34.46,
                Price100ml = 80.06,
                CategoryId = 4,
                ImageUrl = ""
            },
            new Product()
            {
                Id = 8,
                Brand = "Phlov",
                Title = "Take control, Babe!",
                Description = "Intensive balm against stretch marks",
                Price = 27.02,
                Price100ml = 54.82,
                CategoryId = 4,
                ImageUrl = ""
            }
        );
    }
}