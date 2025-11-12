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
    private readonly Mock<IJwtService> _jwtService;
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
        _jwtService = new Mock<IJwtService>();

        _userService = new UserService(
            _repositoryMock.Object,
            _hashServiceMock.Object,
            _jwtService.Object
        );

        _user = _userDto.MapToUser("123");
    }

    [Fact]
    public async Task ShouldAddUserOnRepositoryAsync()
    {
        // Arrange
        _hashServiceMock.Setup(service => service.CreateHash(_userDto.Password)).Returns("123");

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
        _hashServiceMock.Setup(service => service.CreateHash(_userDto.Password)).Returns("123");
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
        _hashServiceMock.Setup(service => service.CreateHash(_userDto.Password)).Returns("123");
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

    [Fact]
    public async Task ShouldReturnsAllUsers()
    {
        // Arrange
        var mockUsersList = new List<User> { _user, _user };
        _repositoryMock.Setup(repository => repository.GetAll()).ReturnsAsync(mockUsersList);

        // Action
        var users = await _userService.GetAll();

        //Assert
        Assert.IsType<List<User>>(users);
        Assert.Equal(2, users.Count);
    }

    [Fact]
    public async Task ShouldAuthenticateUser()
    {
        // Arrange
        var userEmail = _faker.Person.Email;
        var userPassword = "changeme";
        _repositoryMock
            .Setup(repository => repository.GetUserByEmail(userEmail))
            .ReturnsAsync(_user);
        _hashServiceMock.Setup(service => service.CreateHash(userPassword)).Returns(_user.Password);
        _hashServiceMock
            .Setup(service => service.CompareHash(userPassword, _user.Password))
            .Returns(true);
        _jwtService.Setup(service => service.GenerateJwtToken(_user)).Returns("olamundo");

        // Action
        var userToken = await _userService.Authenticate(_user.Email, userPassword);

        // Assert
        Assert.NotEmpty(userToken);
    }

    [Fact]
    public async Task ShouldNotAuthenticateUserIfEmailIncorrect()
    {
        // Arrange
        var userEmail = _faker.Person.Email;
        var userPassword = "changeme";
        _hashServiceMock.Setup(service => service.CreateHash(userPassword)).Returns(_user.Password);
        _hashServiceMock
            .Setup(service => service.CompareHash(userPassword, _user.Password))
            .Returns(true);

        // Action
        var actionAssert = async () =>
        {
            var userToken = await _userService.Authenticate(_user.Email, userPassword);
        };

        // Assert
        (await Assert.ThrowsAsync<Exception>(actionAssert)).WithMessage(
            "Usuário ou senha inválido"
        );
    }

    [Fact]
    public async Task ShouldNotAuthenticateUserIfPasswordIncorrect()
    {
        // Arrange
        var userEmail = _faker.Person.Email;
        var userPassword = "changeme";
        _repositoryMock
            .Setup(repository => repository.GetUserByEmail(userEmail))
            .ReturnsAsync(_user);
        _hashServiceMock.Setup(service => service.CreateHash(userPassword)).Returns(_user.Password);
        _hashServiceMock
            .Setup(service => service.CompareHash(userPassword, _user.Password))
            .Returns(false);

        // Action
        var actionAssert = async () =>
        {
            var userToken = await _userService.Authenticate(_user.Email, userPassword);
        };

        // Assert
        (await Assert.ThrowsAsync<Exception>(actionAssert)).WithMessage(
            "Usuário ou senha inválido"
        );
    }
}
