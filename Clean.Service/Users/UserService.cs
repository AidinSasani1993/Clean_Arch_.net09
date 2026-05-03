using Clean.Application.Dtos.Users;
using Clean.Application.Repositories;
using Clean.Application.Services.Users;
using Clean.Common.Exceptions;
using Clean.Common.Extentions;
using Clean.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<GetCreateUserDto> CreateAsync(CreateUserDto dto)
        {
            if (dto.BirthDay.AddYears(18) > DateTime.Today) 
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
            return new GetCreateUserDto { ErrorCode = 200, Message = $"UserName : {user.Username}  userId : {user.Id}" };

        }

        public async Task<GetTokenDto> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user is null) return null;

            //if (user.Password != HashPassword(dto.Password)) return null;
            if (user.Password != dto.Password) return null;

            var map = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDay = user.BirthDay,
                PhoneNumber = user.PhoneNumber,
                SexType = user.SexType,
                IsActive = user.IsActive,
                Email = user.Email,
                UserName = user.Username,
                Role = user.RoleRef,
            };

            var claims = new List<Claim>
            {
                new Claim("UserName",map.UserName),
                new Claim("FirsName",map.FirstName),
                new Claim("LastName",map.LastName),
                new Claim ("Phone",map.PhoneNumber),
                //new Claim("Email",map.Email,"string"),
                new Claim("Email",map.Email),
                new Claim("SexType",map.SexType.ToString()),
                new Claim("RoleRef",map.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            
            return new GetTokenDto { AccessToken = result };
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
