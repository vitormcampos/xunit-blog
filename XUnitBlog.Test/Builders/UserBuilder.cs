using Bogus;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Test.Builders;

internal class UserBuilder
{
    private Faker faker;
    private string _validFirstName;
    private string _validLastName;
    private string _validEmail;
    private string _validPassword;
    private Role _validRole;
    private string _validUserName;
    private string _validPhoto;

    public UserBuilder()
    {
        faker = new Faker();
        _validFirstName = faker.Name.FirstName();
        _validLastName = faker.Name.LastName();
        _validEmail = faker.Person.Email;
        _validPassword = faker.Random.Word();
        _validRole = Role.ADMIN;
        _validUserName = faker.Person.UserName;
        _validPhoto = faker.Person.Avatar;
    }

    public static UserBuilder New()
    {
        return new UserBuilder();
    }

    public User Build()
    {
        return new User(
            _validFirstName,
            _validLastName,
            _validEmail,
            _validPassword,
            _validRole,
            _validUserName,
            _validPhoto
        );
    }

    public UserBuilder WithFirstName(string firstName)
    {
        _validFirstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastname)
    {
        _validLastName = lastname;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _validEmail = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        _validPassword = password;
        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        _validRole = role;
        return this;
    }

    public UserBuilder WithUserName(string username)
    {
        _validUserName = username;
        return this;
    }

    public UserBuilder WithPhoto(string photo)
    {
        _validPhoto = photo;
        return this;
    }
}
