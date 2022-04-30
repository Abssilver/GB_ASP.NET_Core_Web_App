using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.Datalayer.Abstractions.Entities;
using Authentication.Datalayer.Abstractions.Repositories;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Authentication.DataLayer.Repositories
{
    public sealed class UsersRepository : IUserRepository
    {
        private readonly ApplicationDataContext _context;
        private readonly DbSet<User> _users;

        public UsersRepository(ApplicationDataContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task<User> GetUserAsync(string login, string passwordHash)
        {
            return await _users
                    .Where(entity=> entity.Login.Equals(login) && entity.Password == passwordHash)
                    .FirstOrDefaultAsync(CancellationToken.None);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _users
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(CancellationToken.None);
        }

        public async Task CreateAsync(User item)
        {
            await _users.AddAsync(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(User item)
        {
            _users.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetUserByIdAsync(id);
            if (entity is null)
            {
                return;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateUserRefreshTokenAsync(Guid id, RefreshToken token)
        {
            var entity = await GetUserByIdAsync(id);
            if (entity is null)
            {
                return;
            }
            var refreshToken = entity.LatestRefreshToken;
            refreshToken.Token = token.Token;
            refreshToken.Expires = token.Expires;
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}