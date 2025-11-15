using Bogus;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Exceptions;
using XUnitBlog.Test.Extensions;

namespace XUnitBlog.Test.Posts;

public class PostTests
{
    private readonly Faker _faker;
    private readonly Post _post;

    public PostTests()
    {
        _faker = new Faker();
        _post = new Post(
            _faker.Random.Words(2),
            _faker.Lorem.Sentence(),
            _faker.Image.PicsumUrl(),
            1
        )
        { };
    }

    [Fact]
    public void ShouldCreateAPost()
    {
        var title = "title";
        var content = "content";
        var thumbnail = "";
        var userId = 1;

        var post = new Post(title, content, thumbnail, userId);

        Assert.NotNull(post);
        Assert.Equal(title, post.Title);
        Assert.Equal(content, post.Content);
        Assert.Equal(thumbnail, post.Thumbnail);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowIfTitleIsInvalid(string title)
    {
        var content = "content";
        var thumbnail = "";
        var userId = 1;

        void assertAction()
        {
            var post = new Post(title, content, thumbnail, userId);
        }

        Assert.Throws<DomainModelException>(assertAction).WithMessage("Title is required");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ShouldThrowIfUserIdIsInvalid(long userId)
    {
        var title = "title";
        var content = "content";
        var thumbnail = "";

        void assertAction()
        {
            var post = new Post(title, content, thumbnail, userId);
        }

        Assert.Throws<DomainModelException>(assertAction).WithMessage("User id is invalid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldCreateAPostWhenContentIsEmpty(string content)
    {
        var title = "title";
        var thumbnail = "";
        var userId = 1;

        var post = new Post(title, content, thumbnail, userId);

        Assert.NotNull(post);
        Assert.Equal(title, post.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldCreateAPostWhenThumbnailIsEmpty(string thumbnail)
    {
        var title = "title";
        var content = "hello world";
        var userId = 1;

        var post = new Post(title, content, thumbnail, userId);

        Assert.NotNull(post);
        Assert.Equal(content, post.Content);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowIfChangeTitleWithInvalidValue(string title)
    {
        void assertAction()
        {
            _post.SetTitle(title);
        }

        Assert.Throws<DomainModelException>(assertAction).WithMessage("Title is required");
    }

    [Fact]
    public async Task ShouldSetUpdatedAtWhenPostIsModified()
    {
        var postUpdatedAtBeforeChanged = _post.UpdatedAt;

        await Task.Delay(TimeSpan.FromSeconds(1));
        _post.SetContent(_faker.Lorem.Sentence());

        Assert.NotEqual(postUpdatedAtBeforeChanged, _post.UpdatedAt);
    }
}
