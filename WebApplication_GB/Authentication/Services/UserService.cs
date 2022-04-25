using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Models;
using Authentication.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDictionary<string, AuthResponse> _users = new Dictionary<string, AuthResponse>()
            {
                { "test", new AuthResponse { Password = "test" } }
            };


        public TokenResponse Authenticate(string user, string password)

        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var tokenResponse = new TokenResponse();
            var i = 0;
            foreach (KeyValuePair<string, AuthResponse> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, user) == 0 &&
                    string.CompareOrdinal(pair.Value.Password, password) == 0)
                {
                    tokenResponse.Token = GenerateJwtToken(i, 15);
                    var refreshToken = GenerateRefreshToken(i);
                    pair.Value.LatestRefreshToken = refreshToken;
                    tokenResponse.RefreshToken = refreshToken.Token;
                    return tokenResponse;
                }
            }

            return null;
        }

        public string RefreshToken(string token)
        {
            var i = 0;
            foreach (KeyValuePair<string, AuthResponse> pair in _users)
            {
                i++;
                if
                    (string.CompareOrdinal(pair.Value.LatestRefreshToken.Token, token) == 0
                     && !pair.Value.LatestRefreshToken.IsExpired)
                {
                    pair.Value.LatestRefreshToken = GenerateRefreshToken(i);
                    return pair.Value.LatestRefreshToken.Token;
                }
            }

            return string.Empty;
        }

        private string GenerateJwtToken(int id, int minutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secrets").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new
                    SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(int id)
        {
            var refreshToken = new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360),
                Token = GenerateJwtToken(id, 360)
            };
            return refreshToken;
        }
    }
}