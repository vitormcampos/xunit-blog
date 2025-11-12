using XUnitBlog.Domain.Entities;
using XUnitBlog.Test.Builders;
using XUnitBlog.Test.Extensions;

namespace XUnitBlog.Test.Users;

public class UserTest
{
    [Fact]
    public void ShouldCreateUser()
    {
        // Action
        var user = UserBuilder.New().Build();

        // Assert
        Assert.IsType<User>(user);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserFirstNameIsEmpty(string firstName)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithFirstName(firstName).Build();
        }

        // Assert
        Assert
            .Throws<ArgumentException>(assertAction)
            .WithMessage("First name and last name are required");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserLastNameIsEmpty(string lastName)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithLastName(lastName).Build();
        }

        // Assert
        Assert
            .Throws<ArgumentException>(assertAction)
            .WithMessage("First name and last name are required");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserEmailIsEmpty(string email)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithEmail(email).Build();
        }

        // Assert
        Assert.Throws<ArgumentException>(assertAction).WithMessage("E-mail is required");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("olamundo")]
    [InlineData("olamundo@localhost")]
    public void ShouldThrowWhenUserEmailIsInvalid(string email)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithEmail(email).Build();
        }

        // Assert
        Assert.Throws<ArgumentException>(assertAction).WithMessage("E-mail format is invalid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserPasswordIsEmpty(string password)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithPassword(password).Build();
        }

        // Assert
        Assert.Throws<ArgumentException>(assertAction).WithMessage("Password is required");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserUsernameIsEmpty(string userName)
    {
        // Action
        void assertAction()
        {
            var user = UserBuilder.New().WithUserName(userName).Build();
        }

        // Assert
        Assert.Throws<ArgumentException>(assertAction);
    }

    [Fact(Skip = "Implement a check to see if the password is a valid format.")]
    public void ShouldPhotoContainsImageFormat() { }
}
