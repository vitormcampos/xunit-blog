using System.Text.RegularExpressions;

namespace XUnitBlog.Domain.Entities;

public class User
{
    public long Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string UserName { get; private set; }
    public Role Role { get; private set; }
    public string Photo { get; private set; }

    public User(
        string firstName,
        string lastName,
        string email,
        string password,
        Role role,
        string userName,
        string photo
    )
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("First name and last name are required");
        }

        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("E-mail is required");
        }

        if (!Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
        {
            throw new ArgumentException("E-mail format is invalid");
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password is required");
        }

        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("Username is required");
        }

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
        UserName = userName;
        Photo = photo;
    }

    public void WithFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void WithLastName(string lastName)
    {
        LastName = lastName;
    }

    public void WithPassword(string password)
    {
        Password = password;
    }

    public void WithPhoto(string photo)
    {
        Photo = photo;
    }
}
