using XUnitBlog.Domain.Dtos.Post;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Domain.Services;

public class PostService(IPostRepository postRepository)
{
    public async Task AddAsync(CreatePostDto postDto)
    {
        var post = postDto.MapToPost();

        await postRepository.AddAsync(post);
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
