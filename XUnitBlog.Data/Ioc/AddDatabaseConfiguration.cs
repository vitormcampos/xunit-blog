using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace XUnitBlog.Data.Ioc;

public static class AddDatabaseConfiguration
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("BlogConnectionString"));
        });
    }

    public static void ConfigureDevelopmentMigrations(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
