using System;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.Datalayer.Abstractions.Entities;

namespace Authentication.Datalayer.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string login, string passwordHash);
        Task<User> GetUserByIdAsync(Guid id);
        Task CreateAsync(User item);
        Task UpdateAsync(User item);
        Task DeleteAsync(Guid id);
        Task UpdateUserRefreshTokenAsync(Guid id, RefreshToken token);
    }
}