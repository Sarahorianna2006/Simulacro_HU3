using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Data;

namespace webProductos.Infrastructure.Seed;

public static class SeedAdmin
{
    public static void Seed(AppDbContext context)
    {
        context.Database.Migrate();

        if (!context.Users.Any(u => u.Role == "Admin"))
        {
            var admin = new User
            {
                Username = "admin",
                Email = "admin@local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), 
                Role = "Admin"
            };
            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}