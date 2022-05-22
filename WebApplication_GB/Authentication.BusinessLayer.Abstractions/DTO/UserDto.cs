using System.Collections.Generic;
using System.Security.Claims;
using Authentication.BusinessLayer.Abstractions.Models;

namespace Authentication.BusinessLayer.Abstractions.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public IReadOnlyCollection<Claim> Claims { get; set; }
        public RefreshToken LatestRefreshToken { get; set; }
    }
}