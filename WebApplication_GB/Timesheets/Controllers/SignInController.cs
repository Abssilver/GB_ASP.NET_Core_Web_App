using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Services;
using BusinessLogic.Abstractions.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Requests;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignInController: ControllerBase
    {
        private readonly IUserService _userService;

        public SignInController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
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