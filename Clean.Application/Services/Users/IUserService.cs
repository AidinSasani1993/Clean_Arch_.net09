using Clean.Application.Dtos.Users;

namespace Clean.Application.Services.Users
{
    public interface IUserService
    {
        Task<string> CreateAsync(CreateUserDto dto);
    }
}
