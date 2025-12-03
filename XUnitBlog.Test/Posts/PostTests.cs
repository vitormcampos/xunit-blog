using Bogus;
using XUnitBlog.Domain.Dtos.Users;
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
    public void ShouldThrowWhenSetTitleIsInvalid(string title)
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

    [Fact]
    public void ShouldCreatePinnedPost()
    {
        // Action
        var post = PostBuilder.New().IsPinned(true).Build();

        // Assert
        Assert.True(post.Pinned);
    }

    [Fact]
    public void ShouldPinAnExistingPost()
    {
        // Arrange
        var post = PostBuilder.New().IsPinned(false).Build();

        // Action
        post.TogglePinned(true);

        // Assert
        Assert.True(post.Pinned);
    }

    [Fact]
    public void ShouldUpdatePostStatus()
    {
        // Arange
        var post = PostBuilder.New().Build();
        var expectedStatus = Domain.Entities.PostStatuses.Published;

        // Action
        post.SetStatus(Domain.Entities.PostStatuses.Published);

        // Assert
        Assert.Equal(expectedStatus, post.PostStatus);
    }

    [Fact]
    public void ShouldUpdatePost()
    {
        // Arrange
        var post = PostBuilder.New().Build();
        var newTitle = _faker.Lorem.Sentence();
        var newContent = _faker.Lorem.Paragraph();
        var newThumbnail = _faker.Image.PicsumUrl();
        var newPinnedStatus = false;
        var postStatus = Domain.Entities.PostStatuses.Draft;
        var user = new GetUserDto
        {
            Id = post.UserId,
            FirstName = _faker.Name.FullName(),
            Email = _faker.Internet.Email(),
        };

        // Action
        post.Update(newTitle, newContent, newThumbnail, newPinnedStatus, postStatus, user);

        // Assert
        Assert.Equal(newTitle, post.Title);
        Assert.Equal(newContent, post.Content);
        Assert.Equal(newThumbnail, post.Thumbnail);
    }

    [Fact]
    public void ShouldThrowWhenUpdatingPostPinnedStatusByUnauthorizedUser()
    {
        // Arrange
        var post = PostBuilder.New().IsPinned(false).Build();
        var newTitle = _faker.Lorem.Sentence();
        var newContent = _faker.Lorem.Paragraph();
        var newThumbnail = _faker.Image.PicsumUrl();
        var newPinnedStatus = true;
        var postStatus = Domain.Entities.PostStatuses.Published;
        var user = new GetUserDto
        {
            FirstName = _faker.Name.FullName(),
            Email = _faker.Internet.Email(),
            Role = Domain.Entities.Role.EDITOR, // invalid role to pin
        };

        // Action
        void assertAction()
        {
            post.Update(newTitle, newContent, newThumbnail, newPinnedStatus, postStatus, user);
        }

        // Assert
        Assert
            .Throws<DomainModelException>(assertAction)
            .WithMessage("Just ADMIN users can change the post fixed status");
    }
}
