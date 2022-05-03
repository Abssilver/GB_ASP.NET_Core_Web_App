using Authentication.Datalayer.Abstractions.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.DataLayer
{
    public class AuthenticationDataContext : IdentityDbContext<User>
    {
        public AuthenticationDataContext(DbContextOptions<AuthenticationDataContext> options) : base(options)
        {

        }
    }
}