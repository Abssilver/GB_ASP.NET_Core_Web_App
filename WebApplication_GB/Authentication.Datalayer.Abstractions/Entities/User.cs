using System;
using System.Collections.Generic;
using System.Security.Claims;
using Authentication.BusinessLayer.Abstractions.Models;

namespace Authentication.Datalayer.Abstractions.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public RefreshToken LatestRefreshToken { get; set; }
    }
}