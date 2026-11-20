
using AngeCafe.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Data
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            IConfiguration configuration)
        {
            string adminUsername =
                configuration["SeedUsers:AdminUsername"] ?? "";

            string adminPassword =
                configuration["SeedUsers:AdminPassword"] ?? "";

            string waiterUsername =
                configuration["SeedUsers:WaiterUsername"] ?? "";

            string waiterPassword =
                configuration["SeedUsers:WaiterPassword"] ?? "";

            if (string.IsNullOrWhiteSpace(adminUsername) ||
                string.IsNullOrWhiteSpace(adminPassword) ||
                string.IsNullOrWhiteSpace(waiterUsername) ||
                string.IsNullOrWhiteSpace(waiterPassword))
            {
                throw new InvalidOperationException(
                    "Admin and Waiter seed credentials must be configured."
                );
            }

            var hasher = new PasswordHasher<User>();

            await CreateUserIfMissing(
                context,
                hasher,
                adminUsername,
                adminPassword,
                "Admin"
            );

            await CreateUserIfMissing(
                context,
                hasher,
                waiterUsername,
                waiterPassword,
                "Waiter"
            );

            await context.SaveChangesAsync();
        }

        private static async Task CreateUserIfMissing(
            AppDbContext context,
            PasswordHasher<User> hasher,
            string username,
            string password,
            string role)
        {
            username = username.Trim();

            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
            {
                return;
            }

            var user = new User
            {
                Username = username,
                Role = role
            };

            user.PasswordHash = hasher.HashPassword(
                user,
                password
            );

            context.Users.Add(user);
        }
    }
}
