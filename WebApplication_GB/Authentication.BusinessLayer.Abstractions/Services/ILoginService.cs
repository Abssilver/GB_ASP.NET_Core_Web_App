using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Models;
using BusinessLogic.Abstractions.DTO;

namespace Authentication.BusinessLayer.Abstractions.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Authenticate(UserDto user);
    }
}