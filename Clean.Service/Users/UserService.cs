using Clean.Application.Dtos.Users;
using Clean.Application.Repositories;
using Clean.Application.Services.Users;
using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Clean.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetCreateUserDto> CreateAsync(CreateUserDto dto)
        {
            if (dto.BirthDay.AddYears(18) < DateTime.Today) 
            {
                return new GetCreateUserDto { ErrorCode = 400, Message = ErrorMessage.PersonOlde };
            }

            bool fName = dto.FirstName.IsJustPersianWord();
            if (!fName)
            {
                return new GetCreateUserDto { ErrorCode = 400, Message = ErrorMessage.FirstName };
            }

            var user = new User(dto.UserName, dto.Email, dto.Password, dto.FirstName, 
                dto.LastName, dto.BirthDay, dto.PhoneNumber, dto.SexType, dto.IsActive);

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            return new GetCreateUserDto { ErrorCode = 200, Message = $"UserName : {user.Username}" };

        }
    }
}
