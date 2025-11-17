using Bogus;
using Moq;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;
using XUnitBlog.Domain.Services;
using XUnitBlog.Test._Builders;

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
        _postRepository.Setup(repository => repository.GetById(postId)).ReturnsAsync(currentPost);

        // Action
        await _postService.UpdateAsync(postId, postDto);

        // Assert
        _postRepository.Verify(repository => repository.GetById(It.IsAny<long>()));
        _postRepository.Verify(repository =>
            repository.UpdateAsync(It.IsAny<long>(), It.IsAny<Post>())
        );
    }

    [Fact]
    public async Task ShouldThrowIfUpdatingNonexistentPost()
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
        _postRepository.Setup(repository => repository.GetById(postId)).ReturnsAsync(() => null);

        // Action
        async Task assertAction()
        {
            await _postService.UpdateAsync(postId, postDto);
        }

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(assertAction);
        _postRepository.Verify(repository => repository.GetById(It.IsAny<long>()));
    }
}
