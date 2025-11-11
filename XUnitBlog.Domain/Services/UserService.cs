using XUnitBlog.Domain.Dtos;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Domain.Services;

public class UserService(IUserRepository repository, IHashService hashService)
{
    public async Task AddAsync(CreateUserDto userDto)
    {
        var hashPassword = hashService.CreateHash(userDto.Password);

        var user = userDto.MapToUser(hashPassword);

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

    public async Task<User> UpdateAsync(int userId, UpdateUserDto userDto)
    {
        var user = await repository.GetUserById(userId);

        if (user is null)
        {
            throw new ArgumentException("Usuário não encontrado");
        }

        if (userDto.FirstName != user.FirstName)
        {
            user.WithFirstName(userDto.FirstName);
        }

        if (userDto.LastName != user.LastName)
        {
            user.WithLastName(userDto.LastName);
        }

        if (userDto.Photo != user.Photo)
        {
            user.WithPhoto(userDto.Photo);
        }

        await repository.UpdateAsync(user);

        return user;
    }
}
