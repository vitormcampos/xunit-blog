using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Repositories;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task<IList<Post>> GetAll();
    Task<IList<Post>> GetAllByUser(long userId);
    Task<IList<Post>> GetAllPinnedPosts();
    Task<Post> GetById(long id);
    Task UpdateAsync(long postId, Post post);
}
