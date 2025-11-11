using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUserName(string userName);
    Task<User?> GetUserByEmail(string email);
    Task AddAsync(User user);
}
