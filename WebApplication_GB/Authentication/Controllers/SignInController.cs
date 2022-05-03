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

        /// <summary>
        /// Производит регистрацию пользователя в системе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/signin/registration
        /// 
        ///     Body: { "Username": "BigBob" , "Password": "NoOneHackMeAnyMore", "Role": "User" }
        /// 
        /// </remarks>
        /// <param name="request">Данные запроса по пользователю, который подлежит регистрации</param>
        /// <returns>Зарегистрирован пользователь или нет</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
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