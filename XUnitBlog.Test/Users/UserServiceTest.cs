using Bogus;
using Moq;
using XUnitBlog.Domain.Dtos;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;
using XUnitBlog.Domain.Services;
using XUnitBlog.Test.Extensions;

namespace XUnitBlog.Test.Users;

public class UserServiceTest
{
    private readonly Faker _faker;
    private readonly CreateUserDto _userDto;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly Mock<IHashService> _hashServiceMock;
    private readonly UserService _userService;
    private readonly User _user;

    public UserServiceTest()
    {
        // Arange
        _faker = new Faker();
        _userDto = new CreateUserDto
        {
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Email = _faker.Person.Email,
            Role = "ADMIN",
            Password = "123",
            ConfirmPassword = "123",
            UserName = _faker.Person.UserName,
        };

        _repositoryMock = new Mock<IUserRepository>();
        _hashServiceMock = new Mock<IHashService>();
        _hashServiceMock.Setup(service => service.CreateHash(_userDto.Password)).Returns("123");

        _userService = new UserService(_repositoryMock.Object, _hashServiceMock.Object);

        _user = _userDto.MapToUser("123");
    }

    [Fact]
    public async Task ShouldAddUserOnRepositoryAsync()
    {
        // Action
        await _userService.AddAsync(_userDto);

        // Assert
        _repositoryMock.Verify(c => c.AddAsync(It.Is<User>(u => u.Email == _user.Email)));
    }

    [Fact]
    public async Task ShouldThrowsIfUserEmailExists()
    {
        // Arrange
        var alreadyExistsEmail = _user.Email;
        _repositoryMock.Setup(r => r.GetUserByEmail(alreadyExistsEmail)).ReturnsAsync(_user);

        // Action
        async Task assertAction()
        {
            await _userService.AddAsync(_userDto);
        }

        // Assert
        await Assert
            .ThrowsAsync<Exception>(assertAction)
            .WithMessageAsync("Endereço de e-mail já utilizado");
    }

    [Fact]
    public async Task ShouldThrowsIfUserNameExists()
    {
        // Arrange
        var alreadyUsernameEmail = _userDto.UserName;
        _repositoryMock.Setup(r => r.GetUserByUserName(alreadyUsernameEmail)).ReturnsAsync(_user);

        // Action
        async Task assertAction()
        {
            await _userService.AddAsync(_userDto);
        }

        // Assert
        await Assert
            .ThrowsAsync<Exception>(assertAction)
            .WithMessageAsync("Nome de usuário já utilizado");
    }

    [Fact]
    public async Task ShouldThrowsIfUserRoleIsInvalidAsync()
    {
        var invalidRole = "PUBLISHER";
        var userDto = new CreateUserDto
        {
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Email = _faker.Person.Email,
            Role = invalidRole,
            Password = "123",
            ConfirmPassword = "123",
            UserName = _faker.Person.UserName,
        };

        // Action
        async Task assertAction()
        {
            await _userService.AddAsync(userDto);
        }

        // Assert
        await Assert
            .ThrowsAsync<ArgumentException>(assertAction)
            .WithMessageAsync("Permissão inválida");
    }

    [Fact]
    public void ShouldThrowsIfPasswordAndConfirmPasswordAreDifferent()
    {
        // Arange
        var userDto = new CreateUserDto
        {
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Email = _faker.Person.Email,
            Role = "ADMIN",
            Password = "1234",
            ConfirmPassword = "123",
            UserName = _faker.Person.UserName,
        };

        // Action
        var assertAction = () =>
        {
            var user = userDto.MapToUser("123");
        };

        // Assert
        Assert.Throws<ArgumentException>(assertAction).WithMessage("Senhas não conferem");
    }

    [Fact]
    public async Task ShouldUpdateUserAsync()
    {
        // Arange
        var userId = 1;
        var userDto = new UpdateUserDto
        {
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Photo = _faker.Person.Avatar,
        };

        // Action
        _repositoryMock.Setup(repository => repository.GetUserById(userId)).ReturnsAsync(_user);
        var updatedUser = await _userService.UpdateAsync(userId, userDto);

        // Assert
        Assert.Equal(userDto.FirstName, updatedUser.FirstName);
        Assert.Equal(userDto.LastName, updatedUser.LastName);
        Assert.Equal(userDto.Photo, updatedUser.Photo);
    }
}
