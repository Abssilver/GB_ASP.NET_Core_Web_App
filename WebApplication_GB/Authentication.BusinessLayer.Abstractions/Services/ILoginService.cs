using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using Authentication.BusinessLayer.Abstractions.Models;

namespace Authentication.BusinessLayer.Abstractions.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Authenticate(UserDto user);
    }
}