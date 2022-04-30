using System;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.DTO;

namespace Authentication.BusinessLayer.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser(LoginDto login);
        Task<UserDto> GetUserById(Guid userId);
        Task<Guid> RegisterUser(SignInDto signIn);
        Task<string> RefreshToken(string token);
    }
}