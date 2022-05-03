using System;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Datalayer.Abstractions.Entities
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}