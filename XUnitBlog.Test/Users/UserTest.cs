using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Exceptions;
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
        Assert.Throws<DomainModelException>(assertAction).WithMessage("First name is required");
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
        Assert.Throws<DomainModelException>(assertAction).WithMessage("Last name is required");
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
        Assert.Throws<DomainModelException>(assertAction).WithMessage("E-mail format is invalid");
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
        Assert.Throws<DomainModelException>(assertAction).WithMessage("E-mail format is invalid");
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
        Assert.Throws<DomainModelException>(assertAction).WithMessage("Password is required");
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
        Assert.Throws<DomainModelException>(assertAction);
    }

    [Fact]
    public void ShouldUpdateUser()
    {
        // Arrange
        var user = UserBuilder.New().Build();
        var newFirstName = "NewFirstName";
        var newLastName = "NewLastName";
        var newPhoto = "image.png";

        // Action
        user.Update(newFirstName, newLastName, newPhoto);

        // Assert
        Assert.Equal(user.FirstName, newFirstName);
        Assert.Equal(user.LastName, newLastName);
        Assert.Equal(user.Photo, newPhoto);
    }
}
