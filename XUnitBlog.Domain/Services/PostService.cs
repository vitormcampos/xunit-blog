using Microsoft.Extensions.Hosting;
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

    public async Task<Post> UpdateAsync(long postId, UpdatePostDto postDto, GetUserDto loggedInUser)
    {
        var currentPost = await postRepository.GetById(postId);
        if (currentPost is null)
        {
            throw new ArgumentException("Post not found");
        }

        var postPinnedChanged = postDto.Pinned != currentPost.Pinned;
        if (postPinnedChanged && loggedInUser.Role != Role.ADMIN)
        {
            throw new DomainServiceException(
                "Just ADMIN users can change the post fixed status",
                nameof(UpdatePostDto.Pinned)
            );
        }

        if (postPinnedChanged)
        {
            if (postDto.Pinned)
            {
                currentPost.PinPost();
            }
            else
            {
                currentPost.UnpinPost();
            }
        }

        if (currentPost.Title != postDto.Title)
        {
            currentPost.SetTitle(postDto.Title);
        }

        if (currentPost.Content != postDto.Content)
        {
            currentPost.SetContent(postDto.Content);
        }

        if (currentPost.Thumbnail != postDto.Thumbnail)
        {
            currentPost.SetThumbnail(postDto.Thumbnail);
        }

        var updatedPost = await postRepository.UpdateAsync(postId, currentPost);

        return updatedPost;
    }
}
