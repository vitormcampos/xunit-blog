using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.Data.Seeds;

public static class AddDatabaseSeeds
{
    public static async void CreateAdminUserSeed(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        var hashService = scope.ServiceProvider.GetRequiredService<IHashService>();

        var adminEmail = "admin@xunitblog.com";
        var adminPassword = "Admin@123";
        var encryptedPassword = hashService.CreateHash(adminPassword);

        var adminUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
        if (adminUser is not null)
        {
            Console.WriteLine("Admin user already exists. Skipping seeding.");
            return;
        }

        adminUser = new User(
            "Admin",
            "User",
            adminEmail,
            encryptedPassword,
            Role.ADMIN,
            "ADMIN",
            ""
        );

        await dbContext.Users.AddAsync(adminUser);
        await dbContext.SaveChangesAsync();
    }
}
