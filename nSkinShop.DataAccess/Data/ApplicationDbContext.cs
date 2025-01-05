using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Models;

namespace nSkinShop.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Face Care Products", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Body Care Products", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Sun Care Products", DisplayOrder = 3 },
            new Category { Id = 4, Name = "Treatment & Specialty Products", DisplayOrder = 4 }
        );
        

        modelBuilder.Entity<PromoCode>().HasData(
            new PromoCode
            {
                Id = 1,
                Code = "HOLIDAY30",
                DiscountAmount = 30,
                ExpiryDate = new DateTime(2025, 3, 12, 0, 0, 0, DateTimeKind.Utc),
                IsActive = false
            },
            new PromoCode
            {
                Id = 2,
                Code = "SUMMER20",
                DiscountAmount = 20,
                ExpiryDate = new DateTime(2025, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                IsActive = false
            },
            new PromoCode
            {
                Id = 3,
                Code = "WINTER10",
                DiscountAmount = 10,
                ExpiryDate = new DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                IsActive = true
            }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                Name = "Glowing Clinique",
                City = "New York",
                State = "NY",
                StreetAddress = "123 Main Street",
                PhoneNumber = "555-1234",
                ZipCode = "10001"
            },
            new Company
            {
                Id = 2,
                Name = "Sunrise Wellness",
                City = "Los Angeles",
                State = "CA",
                StreetAddress = "456 Sunset Blvd",
                PhoneNumber = "555-5678",
                ZipCode = "90001"
            },
            new Company
            {
                Id = 3,
                Name = "Oceanview Spa",
                City = "Miami",
                State = "FL",
                StreetAddress = "789 Ocean Drive",
                PhoneNumber = "555-9012",
                ZipCode = "33101"
            }
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
                ImageUrl = "images/product/a41669e1-c6b9-4bf2-9e53-304ec63d81c1.png"
            },
            new Product()
            {
                Id = 2, Brand = "Clinique",
                Title = "Smart Clinical Repair Wrinkle Correcting Cream",
                Description = "Nourishing night cream",
                Price = 100.89,
                Price100ml = 205.77,
                CategoryId = 1,
                ImageUrl = "images/product/7116872c-44a3-488c-92c3-1d89dc4f0ba3.png"
            },
            new Product()
            {
                Id = 3, Brand = "Sol de Janeiro",
                Title = "Brazilian Bum Bum Cream",
                Description = "Brazilian body cream",
                Price = 23.55,
                Price100ml = 31.34,
                CategoryId = 2,
                ImageUrl = "images/product/895074bf-6e5e-4d72-adf2-0a4d43b989fd.png"
            },
            new Product()
            {
                Id = 4, Brand = "Rituals",
                Title = "The Ritual of Ayurveda",
                Description = "Shower gel foam",
                Price = 13.55,
                Price100ml = 17.89,
                CategoryId = 2,
                ImageUrl = "images/product/80060dcb-1606-41d7-a92f-81d825c4a935.png"
            },
            new Product()
            {
                Id = 5, Brand = "SuperGoop",
                Title = "Glowscreen",
                Description = "Sun protection filter SPF 30",
                Price = 24.54,
                Price100ml = 50.00,
                CategoryId = 3,
                ImageUrl = "images/product/231bc67d-1e65-4a39-b1e3-88ecc829726e.png"
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
                ImageUrl = "images/product/5014882f-bd56-40b1-a40e-837b359ba6f7.png"
            },
            new Product()
            {
                Id = 7, Brand = "Dr Irena Eris",
                Title = "Body Art Smoothing Body Scrub with Alabaster ",
                Description = "Smoothing peeling",
                Price = 34.46,
                Price100ml = 80.06,
                CategoryId = 4,
                ImageUrl = "images/product/d3b1620e-22b4-47ba-a907-5688a5fcfc6e.png"
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
                ImageUrl = "images/product/a04fb0ca-bda2-4027-b70a-54155d044fa1.png"
            }
        );
    }
}