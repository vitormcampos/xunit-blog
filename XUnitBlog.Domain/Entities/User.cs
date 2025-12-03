using System.Text.RegularExpressions;
using XUnitBlog.Domain.Exceptions;

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
    public ICollection<Post> Posts { get; set; }

    private User() { }

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
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPassword(password);
        SetEmail(email);
        SetUserName(userName);

        Role = role;
        Photo = photo;
    }

    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            throw new DomainModelException("First name is required");
        }

        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            throw new DomainModelException("Last name is required");
        }

        LastName = lastName;
    }

    public void SetPhoto(string photo)
    {
        Photo = photo;
    }

    private void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new DomainModelException("Password is required");
        }

        Password = password;
    }

    private void SetEmail(string email)
    {
        if (
            string.IsNullOrEmpty(email)
            || !Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success
        )
        {
            throw new DomainModelException("E-mail format is invalid");
        }

        Email = email;
    }

    private void SetUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new DomainModelException("Username is required");
        }

        UserName = userName.ToLower();
    }

    public void Update(string firstName, string lastName, string photo)
    {
        if (firstName != FirstName)
        {
            SetFirstName(firstName);
        }

        if (lastName != LastName)
        {
            SetLastName(lastName);
        }

        if (photo != Photo)
        {
            SetPhoto(photo);
        }
    }
}
