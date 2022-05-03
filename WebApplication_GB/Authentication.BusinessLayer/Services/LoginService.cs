using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Authentication.BusinessLayer.Abstractions.DTO;
using Authentication.BusinessLayer.Abstractions.Models;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.BusinessLayer.Extensions;
using Authentication.Datalayer.Abstractions.Repositories;
using Authentication.Models;
using Microsoft.Extensions.Options;

namespace Authentication.BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        private readonly JwtAccessOptions _jwtAccessOptions;
        private readonly JwtRefreshOptions _jwtRefreshOptions;
        
        public LoginService(
            IOptions<JwtAccessOptions> jwtAccessOptions, 
            IOptions<JwtRefreshOptions> jwtRefreshOptions,
            IUserRepository repository)
        {
            _jwtAccessOptions = jwtAccessOptions.Value;
            _jwtRefreshOptions = jwtRefreshOptions.Value;
            _repository = repository;
        }
        
        public async Task<LoginResponse> Authenticate(UserDto user)
        {
            var accessTokenRaw = _jwtAccessOptions.GenerateToken(user.PopulateClaims());
            var accessToken = new JwtSecurityTokenHandler().WriteToken(accessTokenRaw);
            var refreshTokenRaw = _jwtRefreshOptions.GenerateToken(user.PopulateClaims());
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshTokenRaw);

            var loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Expires = refreshTokenRaw.ValidTo,
            };
            await _repository.UpdateUserRefreshTokenAsync(user.Id, refreshTokenEntity);

            return loginResponse;
        }
    }
}