using Clean.Application.Dtos.Users;
using Clean.Application.Services.Users;
using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Clean.Service.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateAsync(CreateUserDto dto)
        {
            if (dto.BirthDay < ) 
            {
                throw new BussinessException("حداقل سن باید 18 سال باشد");
            }

            bool fName = dto.FirstName.IsJustPersianWord();
            if (!fName)
            {
                throw new ValidationException("نام باید فارسی باشد");
            }

            var user = new User
            {
                BirthDay = dto.BirthDay,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.UserName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    return item.Description;
                }
                
            }

            return user.Id.ToString();

        }
    }
}
