using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Test;

public class UserTest
{
    [Fact]
    public void ShouldCreateUser()
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var email = "johnblack@localhost.com";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Action
        var user = new User(firstName, lastName, email, password, role, userName, photo);

        // Assert
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
        Assert.Equal(role, user.Role);
        Assert.Equal(userName, user.UserName);
        Assert.Equal(photo, user.Photo);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserFirstNameIsEmpty(string firstName)
    {
        // Arange
        var lastName = "Black";
        var email = "johnblack@localhost.com";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserLastNameIsEmpty(string lastName)
    {
        // Arange
        var firstName = "John";
        var email = "johnblack@localhost.com";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserEmailIsEmpty(string email)
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }

    [Theory]
    [InlineData("123")]
    [InlineData("olamundo")]
    [InlineData("olamundo@localhost")]
    public void ShouldThrowWhenUserEmailIsInvalid(string email)
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }

    [Fact]
    public void ShouldThrowWhenUserEmailIsValid()
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var email = "johnblack@localhost.com";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Action
        var user = new User(firstName, lastName, email, password, role, userName, photo);

        // Assert
        Assert.IsType<User>(user);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserPasswordIsEmpty(string password)
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var email = "johnblack@localhost.com";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }

    [Fact]
    public void ShouldHashPassword()
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var email = "johnblack@localhost.com";
        var password = "123";
        var role = Role.ADMIN;
        var userName = "johnblack";
        var photo = "/uploads/photo.pngs";

        // Action
        var user = new User(firstName, lastName, email, password, role, userName, photo);

        // Assert
        Assert.Fail();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowWhenUserUsernameIsEmpty(string userName)
    {
        // Arange
        var firstName = "John";
        var lastName = "Black";
        var password = "123";
        var email = "johnblack@localhost.com";
        var role = Role.ADMIN;
        var photo = "/uploads/photo.pngs";

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Action
            var user = new User(firstName, lastName, email, password, role, userName, photo);
        });
    }
}
