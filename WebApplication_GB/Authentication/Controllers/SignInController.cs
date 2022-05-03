using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.Requests;
using BusinessLogic.Abstractions.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignInController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<SignInController> _logger;

        public SignInController(
            IUserService userService,
            ILogger<SignInController> logger)
        {
            _userService = userService;
            _logger = logger;
            
            _logger.LogDebug(1, $"Logger встроен в {this.GetType()}");
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest request)
        {
            var response = await _userService.RegisterUser(new SignInDto
            {
                Username = request.Username,
                Password = request.Password,
                Role = request.Role,
            });

            return Ok(response);
        }
    }
}