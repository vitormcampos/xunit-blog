using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string UserName { get; set; }
    public string Photo { get; set; }

    public User MapToUser()
    {
        if (!Enum.TryParse<Role>(Role, out var role))
        {
            throw new ArgumentException("Permissão inválida");
        }

        return new User(FirstName, LastName, Email, Password, role, UserName, Photo);
    }
}
