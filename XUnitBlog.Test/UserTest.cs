using XUnitBlog.Domain.Entities;
using XUnitBlog.Test.Builders;

namespace XUnitBlog.Test;

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
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithFirstName(firstName).Build();
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserLastNameIsEmpty(string lastName)
    {
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithLastName(lastName).Build();
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserEmailIsEmpty(string email)
    {
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithEmail(email).Build();
        });
    }

    [Theory]
    [InlineData("123")]
    [InlineData("olamundo")]
    [InlineData("olamundo@localhost")]
    public void ShouldThrowWhenUserEmailIsInvalid(string email)
    {
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithEmail(email).Build();
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserPasswordIsEmpty(string password)
    {
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithPassword(password).Build();
        });
    }

    [Fact(Skip = "Implement a check to see if the password is a hash.")]
    public void ShouldHashPassword() { }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserUsernameIsEmpty(string userName)
    {
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = UserBuilder.New().WithUserName(userName).Build();
        });
    }

    [Fact(Skip = "Implement a check to see if the password is a valid format.")]
    public void ShouldPhotoContainsImageFormat() { }
}
