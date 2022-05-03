using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Authentication.BusinessLayer.Abstractions.DTO;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Authentication.BusinessLayer.Extensions
{
    public static class ClaimsExtensions
    {
        public static IEnumerable<Claim> PopulateClaims (this UserDto user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Id),
            };
            claims.AddRange(user.Claims.Where(c => c.Type == ClaimTypes.Role));
            return claims;
        }
    }
}