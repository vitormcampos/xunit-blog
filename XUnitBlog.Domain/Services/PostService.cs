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

    public async Task<Post> Update(long postId, UpdatePostDto postDto, GetUserDto loggedInUser)
    {
        var currentPost = await postRepository.GetById(postId);
        if (currentPost is null)
        {
            throw new ArgumentException("Post not found");
        }

        currentPost.Update(
            postDto.Title,
            postDto.Content,
            postDto.Thumbnail,
            postDto.Pinned,
            postDto.PostStatus,
            loggedInUser
        );

        var updatedPost = await postRepository.Update(currentPost);

        return updatedPost;
    }
}
