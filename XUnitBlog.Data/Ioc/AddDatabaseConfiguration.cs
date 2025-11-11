using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
}
