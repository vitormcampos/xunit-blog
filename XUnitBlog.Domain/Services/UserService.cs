using XUnitBlog.Domain.Dtos;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Domain.Services;

public class UserService
{
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public async Task AddAsync(UserDto userDto)
    {
        var user = userDto.MapToUser();

        var userEmailExists = await repository.GetUserByEmail(user.Email);
        if (userEmailExists is not null)
        {
            throw new Exception("Endereço de e-mail já utilizado");
        }

        var userNameExists = await repository.GetUserByUserName(user.UserName);
        if (userNameExists is not null)
        {
            throw new Exception("Nome de usuário já utilizado");
        }

        await repository.AddAsync(user);
    }
}
