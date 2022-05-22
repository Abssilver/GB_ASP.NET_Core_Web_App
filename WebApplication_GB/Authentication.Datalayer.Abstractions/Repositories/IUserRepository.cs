using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using Authentication.BusinessLayer.Abstractions.Models;

namespace Authentication.Datalayer.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserAsync(string login, string passwordHash);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<bool> CreateAsync(string login, string passwordHash, IEnumerable<Claim> claims);
        Task UpdateUserRefreshTokenAsync(string id, RefreshToken token);
    }
}