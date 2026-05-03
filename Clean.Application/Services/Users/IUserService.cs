using Clean.Application.Dtos.Users;

namespace Clean.Application.Services.Users
{
    public interface IUserService
    {
        Task<GetCreateUserDto> CreateAsync(CreateUserDto dto);
        Task<GetTokenDto> Login(LoginDto dto);
    }
}
