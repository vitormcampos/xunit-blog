using Bogus;
using Moq;
using XUnitBlog.Domain.Dtos.Posts;
using XUnitBlog.Domain.Dtos.Users;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Exceptions;
using XUnitBlog.Domain.Repositories;
using XUnitBlog.Domain.Services;
using XUnitBlog.Test._Builders;
using XUnitBlog.Test.Extensions;

namespace XUnitBlog.Test.Posts;

public class PostServiceTest
{
    private readonly Faker _faker;
    private readonly Mock<IPostRepository> _postRepository;
    private readonly PostService _postService;

    public PostServiceTest()
    {
        _faker = new Faker();
        _postRepository = new Mock<IPostRepository>();
        _postService = new PostService(_postRepository.Object);
    }

    [Fact]
    public async Task ShouldAddPostOnRepository()
    {
        // Arange
        var post = new CreatePostDto
        {
            Title = _faker.Lorem.Sentence(),
            Content = _faker.Lorem.Paragraph(),
            Thumbnail = _faker.Image.PlaceImgUrl(),
            UserId = _faker.Random.Number(1, 100),
        };

        // Action
        await _postService.AddAsync(post);

        // Assert
        _postRepository.Verify(repository => repository.AddAsync(It.IsAny<Post>()));
    }

    [Fact]
    public async Task ShouldThrowWhenAddPostWithInvalidUserIdAsync()
    {
        // Arange
        var post = new CreatePostDto
        {
            Title = _faker.Lorem.Sentence(),
            Content = _faker.Lorem.Paragraph(),
            Thumbnail = _faker.Image.PlaceImgUrl(),
            UserId = 0,
        };

        // Action
        async Task assertAction()
        {
            await _postService.AddAsync(post);
        }

        // Assert
        await Assert
            .ThrowsAsync<DomainModelException>(assertAction)
            .WithMessageAsync("User id is invalid");
    }

    [Fact]
    public async Task ShouldUpdatePostOnRepository()
    {
        // Arange
        var postId = Convert.ToInt64(_faker.Random.Number(1, 10));
        var currentPost = PostBuilder.New().Build();
        var postDto = new UpdatePostDto
        {
            Title = _faker.Lorem.Sentence(),
            Content = _faker.Lorem.Paragraph(),
            Thumbnail = _faker.Image.PlaceImgUrl(),
        };
        var loggedInUser = new GetUserDto { Role = Role.EDITOR };
        _postRepository.Setup(repository => repository.GetById(postId)).ReturnsAsync(currentPost);

        // Action
        await _postService.Update(postId, postDto, loggedInUser);

        // Assert
        _postRepository.Verify(repository => repository.GetById(It.IsAny<long>()));
        _postRepository.Verify(repository => repository.Update(It.IsAny<Post>()));
    }

    [Fact]
    public async Task ShouldThrowWhenUpdatingNonExistentPost()
    {
        // Arange
        var postId = Convert.ToInt64(_faker.Random.Number(1, 10));
        var currentPost = PostBuilder.New().Build();
        var postDto = new UpdatePostDto
        {
            Title = _faker.Lorem.Sentence(),
            Content = _faker.Lorem.Paragraph(),
            Thumbnail = _faker.Image.PlaceImgUrl(),
        };
        var loggedInUser = new GetUserDto { Role = Role.EDITOR };
        _postRepository.Setup(repository => repository.GetById(postId)).ReturnsAsync(() => null);

        // Action
        async Task assertAction()
        {
            await _postService.Update(postId, postDto, loggedInUser);
        }

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(assertAction);
        _postRepository.Verify(repository => repository.GetById(It.IsAny<long>()));
    }

    [Fact]
    public async Task ShouldGetAllPostsFromRepository()
    {
        // Arrange
        var post = PostBuilder.New().Build();
        _postRepository.Setup(repository => repository.GetAll()).ReturnsAsync([post, post]);

        // Action
        var posts = await _postService.GetAll();

        // Assert
        _postRepository.Verify(repository => repository.GetAll());
        Assert.True(posts.Any());
    }

    [Fact]
    public async Task ShouldGetAllPostsForUserFromRepository()
    {
        // Arrange
        var post = PostBuilder.New().Build();
        var userId = 1;
        _postRepository
            .Setup(repository => repository.GetAllByUser(userId))
            .ReturnsAsync([post, post]);

        // Action
        var posts = await _postService.GetAllByUser(userId);

        // Assert
        _postRepository.Verify(repository => repository.GetAllByUser(It.IsAny<long>()));
        Assert.True(posts.Any());
    }

    [Fact]
    public async Task ShouldGetAllPinnedPostsFromRepository()
    {
        // Arrange
        var post = PostBuilder.New().IsPinned(true).Build();
        _postRepository.Setup(repository => repository.GetAllPinnedPosts()).ReturnsAsync([post]);

        // Action
        var pinnedPosts = await _postService.GetAllPinnedPosts();

        // Assert
        _postRepository.Verify(repository => repository.GetAllPinnedPosts());
        Assert.True(pinnedPosts.Any());
        Assert.DoesNotContain(pinnedPosts, p => !p.Pinned);
    }

    [Fact]
    public async Task ShouldReturnEmptyListWhenNotFindPinnedPostsFromRepository()
    {
        // Arrange
        _postRepository
            .Setup(repository => repository.GetAllPinnedPosts())
            .ReturnsAsync(() => null);

        // Action
        var pinnedPosts = await _postService.GetAllPinnedPosts();

        // Assert
        _postRepository.Verify(repository => repository.GetAllPinnedPosts());
        Assert.IsAssignableFrom<IList<Post>>(pinnedPosts);
    }

    [Fact]
    public async Task ShouldGetAPostById()
    {
        // Arrange
        var postId = 1;
        var expectedPost = PostBuilder.New().Build();
        _postRepository
            .Setup(repository => repository.GetById(It.IsAny<long>()))
            .ReturnsAsync(expectedPost);

        // Action
        var post = await _postService.GetById(postId);

        Assert.NotNull(post);
    }
}
