using XUnitBlog.Domain.Dtos.Posts;
using XUnitBlog.Domain.Dtos.Users;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Exceptions;
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

    public async Task<Post> GetById(long postId)
    {
        return await postRepository.GetById(postId);
    }

    public async Task<bool> ValidateUserCanUpdatePinnedFlag(
        GetUserDto user,
        UpdatePostDto updatePostDto,
        long postId
    )
    {
        var post = await postRepository.GetById(postId);
        var postPinnedChanged = updatePostDto.Pinned != post.Pinned;

        if (postPinnedChanged && user.Role != Role.ADMIN)
        {
            throw new DomainServiceException(
                "Just ADMIN users can change the post fixed status",
                nameof(UpdatePostDto.Pinned)
            );
        }

        return true;
    }

    public async Task<Post> UpdateAsync(long postId, UpdatePostDto postDto)
    {
        var currentPost = await postRepository.GetById(postId);
        if (currentPost is null)
        {
            throw new InvalidOperationException();
        }

        var post = postDto.MapToPost(currentPost.UserId);
        var updatedPost = await postRepository.UpdateAsync(postId, post);

        return updatedPost;
    }
}
