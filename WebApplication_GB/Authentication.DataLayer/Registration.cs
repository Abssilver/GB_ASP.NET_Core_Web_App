using Authentication.Datalayer.Abstractions.Entities;
using Authentication.Datalayer.Abstractions.Repositories;
using Authentication.DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.DataLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterAuthenticationDataLayer(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 4;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AuthenticationDataContext>()
                .AddDefaultTokenProviders();
            
            return services.AddTransient<IUserRepository, UsersRepository>();
        }
    }
}