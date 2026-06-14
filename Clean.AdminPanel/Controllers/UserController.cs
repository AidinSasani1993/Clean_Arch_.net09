using Clean.Application.Dtos.Users;
using Clean.Application.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateAsync(CreateUserDto dto)
        {
            var result = await _userService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            _logger.LogInformation("Request For Login", DateTime.UtcNow.ToString());
            var result = await _userService.Login(dto);
            _logger.LogInformation("Respons For Login And Failed", DateTime.UtcNow.ToString());
            return Ok(result);
        }

    }
}
