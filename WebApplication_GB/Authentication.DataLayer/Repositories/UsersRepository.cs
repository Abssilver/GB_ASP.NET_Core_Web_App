using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.Datalayer.Abstractions.Entities;
using Authentication.Datalayer.Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Authentication.DataLayer.Repositories
{
    public sealed class UsersRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UsersRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDto> GetUserAsync(string login, string passwordHash)
        {
            var result = await _signInManager
                .PasswordSignInAsync(login, passwordHash, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(login);
            if (user is null)
            {
                return null;
            }

            var claims = await _userManager.GetClaimsAsync(user);
            return new UserDto
            {
                Id = user.Id,
                Claims = claims.ToImmutableArray(),
                LatestRefreshToken = new RefreshToken
                {
                    Token = user.RefreshToken,
                    Expires = user.TokenExpires,
                }
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return null;
            }
            var claims = await _userManager.GetClaimsAsync(user);
            return new UserDto
            {
                Id = user.Id,
                Claims = claims.ToImmutableArray(),
                LatestRefreshToken = new RefreshToken
                {
                    Token = user.RefreshToken,
                    Expires = user.TokenExpires,
                }
            };
        }

        public async Task<bool> CreateAsync(string login, string passwordHash, IEnumerable<Claim> claims)
        {
            var user = new User
            {
                UserName = login,
                Email = login,
            };
            var createResult = await _userManager.CreateAsync(user, passwordHash);
            if (!createResult.Succeeded)
            {
                return false;
            }
            var claimsResult = await _userManager.AddClaimsAsync(user, claims);
            
            return claimsResult.Succeeded;
        }
        public async Task UpdateUserRefreshTokenAsync(string id, RefreshToken token)
        {
            var entity = await _userManager.FindByIdAsync(id);
            if (entity is null)
            {
                return;
            }
            entity.RefreshToken = token.Token;
            entity.TokenExpires = token.Expires;
            await _userManager.UpdateAsync(entity);
        }
    }
}