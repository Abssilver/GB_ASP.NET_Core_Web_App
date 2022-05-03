using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using Authentication.BusinessLayer.Abstractions.JwtOptions;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.BusinessLayer.Extensions;
using Authentication.Datalayer.Abstractions.Repositories;
using Authentication.Models;
using BusinessLogic.Abstractions.DTO;
using Microsoft.Extensions.Options;

namespace Authentication.BusinessLayer.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtOptions _jwtRefreshOptions;
        
        public UserService(
            IUserRepository repository, 
            IOptions<JwtRefreshOptions> jwtRefreshOptions)
        {
            _repository = repository;
            _jwtRefreshOptions = jwtRefreshOptions.Value;
        }

        public async Task<UserDto> GetUser(LoginDto login)
        {
            var passwordHash = Convert.ToBase64String(GetPasswordHash(login.Password));
            return await _repository.GetUserAsync(login.Username, passwordHash);
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            return await _repository.GetUserByIdAsync(userId);
        }

        public async Task<bool> RegisterUser(SignInDto signIn)
        {
            if (signIn == null)
                throw new ArgumentNullException(nameof(signIn), "Parameter login cannot be null.");
            
            var login = signIn.Username;
            var password = Convert.ToBase64String(GetPasswordHash(signIn.Password));
            
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultRoleClaimType, signIn.Role)
            };

            return await _repository.CreateAsync(login, password, claims);
        }

        public async Task<string> RefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                var security = tokenHandler.ReadJwtToken(token);
                var idClaim = security.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId);
                if (idClaim is null)
                {
                    return string.Empty;
                }
                
                var userId = idClaim.Value;
                var user = await _repository.GetUserByIdAsync(userId);

                if (user is null)
                {
                    return string.Empty; 
                }

                var refreshToken = user.LatestRefreshToken;
                
                if (string.CompareOrdinal(refreshToken.Token, token) == 0 && !refreshToken.IsExpired)
                {
                    var newRefreshTokenRaw = _jwtRefreshOptions.GenerateToken(user.PopulateClaims());
                    var newRefreshToken = new JwtSecurityTokenHandler().WriteToken(newRefreshTokenRaw);
                    
                    var refreshTokenEntity = new RefreshToken
                    {
                        Token = newRefreshToken,
                        Expires = newRefreshTokenRaw.ValidTo,
                    };
                    
                    await _repository.UpdateUserRefreshTokenAsync(user.Id, refreshTokenEntity);
                    return refreshToken.Token;
                }
            }
            
            return string.Empty;
        }

        private static byte[] GetPasswordHash(string password)
        {
            using var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
        }
    }
}