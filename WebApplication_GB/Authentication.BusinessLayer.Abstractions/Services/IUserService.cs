using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using BusinessLogic.Abstractions.DTO;

namespace Authentication.BusinessLayer.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser(LoginDto login);
        Task<UserDto> GetUserById(string userId);
        Task<bool> RegisterUser(SignInDto signIn);
        Task<string> RefreshToken(string token);
    }
}