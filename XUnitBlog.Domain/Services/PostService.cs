using XUnitBlog.Domain.Dtos.Posts;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Domain.Services;

public class PostService(IPostRepository postRepository)
{
    public async Task AddAsync(CreatePostDto postDto)
    {
        var post = postDto.MapToPost();

        await postRepository.AddAsync(post);
    }

    public async Task<IList<Post>> GetAll()
    {
        return await postRepository.GetAll();
    }

    public async Task<IList<Post>> GetAllByUser(long userId)
    {
        return await postRepository.GetAllByUser(userId);
    }

    public async Task<IList<Post>> GetAllPinnedPosts()
    {
        var posts = await postRepository.GetAllPinnedPosts();

        return posts ?? [];
    }

    public async Task UpdateAsync(long postId, UpdatePostDto postDto)
    {
        var currentPost = await postRepository.GetById(postId);

        if (currentPost is null)
        {
            throw new InvalidOperationException();
        }

        var post = postDto.MapToPost(currentPost.UserId);

        await postRepository.UpdateAsync(postId, post);
    }
}
