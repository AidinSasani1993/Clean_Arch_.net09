using Clean.Application.Dtos.Users;

namespace Clean.Application.Services.Users
{
    public interface IUserService
    {
        Task<GetCreateUserDto> CreateAsync(CreateUserDto dto);
    }
}
