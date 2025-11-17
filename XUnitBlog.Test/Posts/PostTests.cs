using Bogus;
using XUnitBlog.Domain.Exceptions;
using XUnitBlog.Test._Builders;
using XUnitBlog.Test.Extensions;

namespace XUnitBlog.Test.Posts;

public class PostTests
{
    private readonly Faker _faker;

    public PostTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public void ShouldCreateAPost()
    {
        // Arrange & Action
        var post = PostBuilder.New().Build();

        // Assert
        Assert.NotNull(post);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowIfTitleIsInvalid(string title)
    {
        // Action
        void assertAction()
        {
            var post = PostBuilder.New().WithTitle(title).Build();
        }

        // Assert
        Assert.Throws<DomainModelException>(assertAction).WithMessage("Title is required");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ShouldThrowIfUserIdIsInvalid(long userId)
    {
        // Action
        void assertAction()
        {
            var post = PostBuilder.New().WithUserId(userId).Build();
        }

        // Assert
        Assert.Throws<DomainModelException>(assertAction).WithMessage("User id is invalid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldCreateAPostWhenContentIsEmpty(string content)
    {
        // Action
        var post = PostBuilder.New().WithContent(content).Build();

        // Assert
        Assert.NotNull(post);
        Assert.Equal(content, post.Content);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldCreateAPostWhenThumbnailIsEmpty(string thumbnail)
    {
        var post = PostBuilder.New().WithThumbnail(thumbnail).Build();

        Assert.NotNull(post);
        Assert.Equal(thumbnail, post.Thumbnail);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowIfChangeTitleWithInvalidValue(string title)
    {
        // Arrange
        var post = PostBuilder.New().Build();

        // Action
        void assertAction()
        {
            post.SetTitle(title);
        }

        // Assert
        Assert.Throws<DomainModelException>(assertAction).WithMessage("Title is required");
    }

    [Fact]
    public async Task ShouldSetUpdatedAtWhenPostIsModified()
    {
        // Arrange
        var post = PostBuilder.New().Build();
        var postUpdatedAtBeforeChanged = post.UpdatedAt;

        // Action
        await Task.Delay(TimeSpan.FromSeconds(1));
        post.SetContent(_faker.Lorem.Sentence());

        // Assert
        Assert.NotEqual(postUpdatedAtBeforeChanged, post.UpdatedAt);
    }
}
