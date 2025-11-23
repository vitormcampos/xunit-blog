using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos.Users;

public class GetUserDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
    public string Photo { get; set; }

    public static GetUserDto MapToDto(User user) =>
        new GetUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Role,
            Photo = user.Photo,
        };
}
