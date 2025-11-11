using Microsoft.Extensions.DependencyInjection;
using XUnitBlog.Data.Repositories;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Data.Ioc;

public static class AddRepositoriesConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
