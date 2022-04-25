using Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication
{

    public static class Registration
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services)
        {
            return services.AddSingleton<IUserService, UserService>();;
        }
    }

}