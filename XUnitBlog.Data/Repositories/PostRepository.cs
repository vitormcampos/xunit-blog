using Microsoft.EntityFrameworkCore;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Data.Repositories;

internal class PostRepository(BlogContext context) : IPostRepository
{
    public async Task AddAsync(Post post)
    {
        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
    }

    public async Task<IList<Post>> GetAll()
    {
        return await context.Posts.AsNoTracking().ToListAsync();
    }

    public async Task<IList<Post>> GetAllByUser(long userId)
    {
        return await context.Posts.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
    }

    public async Task<IList<Post>> GetAllPinnedPosts()
    {
        return await context
            .Posts.Where(p => p.Pinned && p.PostStatus == PostStatuses.Published)
            .AsNoTracking()
            .Take(4)
            .ToListAsync();
    }

    public async Task<Post?> GetById(long id)
    {
        return await context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> Update(Post post)
    {
        await context
            .Posts.Where(p => p.Id == post.Id)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(p => p.Title, post.Title)
                    .SetProperty(p => p.Content, post.Content)
                    .SetProperty(p => p.Thumbnail, post.Thumbnail)
                    .SetProperty(p => p.PostStatus, post.PostStatus)
            );
        await context.SaveChangesAsync();

        return await context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == post.Id);
    }
}
