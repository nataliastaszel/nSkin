using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using nSkinShop.Models;

namespace nSkinShop.Data;

public static  class ApplicationDbInitializer
{
    public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Customer"));
            await roleManager.CreateAsync(new IdentityRole("Company"));
            await roleManager.CreateAsync(new IdentityRole("Employee"));
        }
        
        if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Max",
                City = "New York",
                State = "NY",
                StreetAddress = "122 Street",
                PostalCode = "12345",
            };

            await userManager.CreateAsync(user, "SecureAdminPassword123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }
        
        if (await userManager.FindByEmailAsync("customer@gmail.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                Name = "Cate",
                City = "New York",
                State = "NY",
                StreetAddress = "123 Main Street",
                PostalCode = "12225",
            };

            await userManager.CreateAsync(user, "SecureCustomerPassword123!");
            await userManager.AddToRoleAsync(user, "Customer");
        }
    }
}