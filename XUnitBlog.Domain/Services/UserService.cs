using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using XUnitBlog.Domain.Dtos.Users;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Exceptions;
using XUnitBlog.Domain.Repositories;

namespace XUnitBlog.Domain.Services;

public class UserService(
    IUserRepository repository,
    IHashService hashService,
    IJwtService jwtService
)
{
    public async Task<GetUserDto> GetById(long id)
    {
        var user = await repository.GetUserById(id);

        if (user is null)
        {
            throw new InvalidOperationException("Usuário não encontrado");
        }

        return GetUserDto.MapToDto(user);
    }

    public async Task AddAsync(CreateUserDto userDto)
    {
        var hashPassword = hashService.CreateHash(userDto.Password);

        var user = userDto.MapToUser(hashPassword);

        var userEmailExists = await repository.GetUserByEmail(user.Email);
        if (userEmailExists is not null)
        {
            throw new DomainServiceException("Endereço de e-mail já utilizado");
        }

        var userNameExists = await repository.GetUserByUserName(user.UserName);
        if (userNameExists is not null)
        {
            throw new DomainServiceException("Nome de usuário já utilizado");
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

        user.Update(userDto.FirstName, userDto.LastName, userDto.Photo);
        await repository.UpdateAsync(user);

        return user;
    }

    public async Task<IList<GetUserDto>> GetAll()
    {
        var users = await repository.GetAll();

        return users.Select(GetUserDto.MapToDto).ToList();
    }

    public async Task<string> Authenticate(string email, string password)
    {
        var userExists = await repository.GetUserByEmail(email);
        var passwordCheck = hashService.CompareHash(password, userExists?.Password ?? "");
        if (userExists is null || !passwordCheck)
        {
            throw new DomainServiceException("Usuário ou senha inválido");
        }

        var token = jwtService.GenerateJwtToken(userExists);

        return token;
    }
}
