using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUserName(string userName);
    Task<User?> GetUserByEmail(string email);
    Task AddAsync(User user);
    Task<User?> GetUserById(int userId);
    Task<IList<User>> GetAll();
    Task UpdateAsync(User user);
}
