using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string UserName { get; set; }
    public string Photo { get; set; }

    public User MapToUser(string hashPassword)
    {
        if (!Enum.TryParse<Role>(Role, out var role))
        {
            throw new ArgumentException("Permissão inválida");
        }

        if (string.IsNullOrEmpty(Password))
        {
            throw new ArgumentException("Senha precisa ser informada");
        }

        if (Password != ConfirmPassword)
        {
            throw new ArgumentException("Senhas não conferem");
        }

        return new User(FirstName, LastName, Email, hashPassword, role, UserName, Photo);
    }
}
