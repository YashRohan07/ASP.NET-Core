using Microsoft.AspNetCore.Identity;  // For IdentityRole and UserManager
using Microsoft.Extensions.DependencyInjection; // For resolving scoped services
using System;
using System.Threading.Tasks;
using UserManagement.Models; 

namespace UserManagement.Data
{
    public static class SeedData
    {
        // This method seeds roles and an admin user when the app starts
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Get the RoleManager service from DI to manage roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Get the UserManager service from DI to manage users
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define the roles you want to ensure exist
            string[] roles = new[] { "Admin", "User" };

            // Loop through each role name and create it if it doesn't exist yet
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Create the role in the database
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Define the email for the admin user you want to create
            var adminEmail = "admin@example.com";

            // Try to find the admin user by email
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            // If the admin user does not exist, create one
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,   // Set username to email
                    Email = adminEmail,      // Set email
                    Name = "Administrator",  // Custom name property
                    Age = 30,                // Example age
                    Address = "Admin HQ",    // Example address
                    IsActive = true,         // Mark as active
                    EmailConfirmed = true    // Skip email confirmation for seed user
                };

                // Create the user with a secure default password
                var result = await userManager.CreateAsync(adminUser, "Admin@12345");

                // If creation succeeded, assign the Admin role to the user
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // If creation failed, throw an exception with error details
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors));
                }
            }
        }
    }
}
