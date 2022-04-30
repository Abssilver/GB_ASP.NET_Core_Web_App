using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.Datalayer.Abstractions.Entities;
using Authentication.Datalayer.Abstractions.Repositories;
using Authentication.Models;
using BusinessLogic.Abstractions.DTO;
using Microsoft.Extensions.Options;

namespace Authentication.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtRefreshOptions _jwtRefreshOptions;
        
        public UserService(
            IUserRepository repository, 
            IOptions<JwtRefreshOptions> jwtRefreshOptions)
        {
            _repository = repository;
        }

        public async Task<UserDto> GetUser(LoginDto login)
        {
            var passwordHash = Convert.ToBase64String(GetPasswordHash(login.Password));
            var user = await _repository.GetUserAsync(login.Username, passwordHash);
            return new UserDto
            {
                Id = user.Id,
                Login = user.Login,
                Role = "None",
            };
        }

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            return new UserDto
            {
                Id = user.Id,
                Login = user.Login,
                Role = "None",
            };
        }

        public async Task<Guid> RegisterUser(SignInDto signIn)
        {
            if (signIn == null)
                throw new ArgumentNullException(nameof(signIn), "Parameter login cannot be null.");

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, signIn.Role)
            };
            
            var user = new User
            {
                Id = Guid.NewGuid(), 
                Login = signIn.Username, 
                Password = Convert.ToBase64String(GetPasswordHash(signIn.Password)), 
                Claims = claims,
            };

            await _repository.CreateAsync(user);
            return user.Id;
        }

        public async Task<string> RefreshToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            if (tokenHandler.CanReadToken(token))
            {
                var security = tokenHandler.ReadJwtToken(token);
                var idClaim = security.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
                if (idClaim is null)
                {
                    return string.Empty;
                }
                
                var userId = Guid.Parse(idClaim.Value);
                var user = await _repository.GetUserByIdAsync(userId);

                if (user is null)
                {
                    return string.Empty; 
                }

                var refreshToken = user.LatestRefreshToken;
                
                if (string.CompareOrdinal(refreshToken.Token, token) == 0 && !refreshToken.IsExpired)
                {
                    var newRefreshTokenRaw = _jwtRefreshOptions.GenerateToken(user.Claims);
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