using Authentication.Datalayer.Abstractions.Repositories;
using Authentication.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.DataLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterAuthenticationDataLayer(this IServiceCollection services)
        {
            return services.AddTransient<IUserRepository, UsersRepository>();
        }
    }
}