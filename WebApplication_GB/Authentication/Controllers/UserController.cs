using System;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.Requests;
using BusinessLogic.Abstractions.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;


        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            ILoginService loginService)
        {
            _logger = logger;
            _logger.LogDebug(1, $"Logger встроен в {this.GetType()}");
            _userService = userService;
            _loginService = loginService;
        }

        /// <summary>
        /// Производит выдачу прав пользователю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/user/login
        /// 
        ///     Body: { "Login": "BigBob" , "Password": "NoOneHackMeAnyMore" }
        ///
        /// </remarks>
        /// <param name="request">Данные запроса по пользователю, который входит в систему</param>
        /// <returns>Токены доступа и обновления</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователен не найден</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var user = await _userService.GetUser(new LoginDto
            { 
                Username = request.Login, 
                Password = request.Password,
            });

            if (user == null)
            {
                return Unauthorized();
            }

            var response = await _loginService.Authenticate(user);
            
            if (response is null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        /// <summary>
        /// Производит обноление в БД refresh токена. Старый берется из cookie
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST url:port/api/user/refresh-token
        /// 
        /// </remarks>
        /// <returns>Новый токен обновления</returns>
        /// <response code="200">Все хорошо</response>
        /// <response code="400">Передали неправильные параметры</response>
        /// <response code="401">Пользователь не прошел аутентификацию</response>
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            var oldRefreshToken = Request.Cookies["refreshToken"];
            var newRefreshToken = await _userService.RefreshToken(oldRefreshToken);
            if (string.IsNullOrEmpty(newRefreshToken))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            SetTokenCookie(newRefreshToken);
            return Ok(newRefreshToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}