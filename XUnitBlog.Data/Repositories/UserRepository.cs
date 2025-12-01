using Microsoft.EntityFrameworkCore;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Data.Repositories;

internal class UserRepository(BlogContext blogContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await blogContext.Users.AddAsync(user);
        await blogContext.SaveChangesAsync();
    }

    public async Task<IList<User>> GetAll()
    {
        return await blogContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await blogContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserById(long userId)
    {
        return await blogContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await blogContext
            .Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task UpdateAsync(User user)
    {
        await blogContext
            .Users.Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(u => u.FirstName, user.FirstName)
                    .SetProperty(u => u.LastName, user.LastName)
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.UserName, user.UserName)
                    .SetProperty(u => u.Photo, user.Photo)
            );
        await blogContext.SaveChangesAsync();
    }
}
