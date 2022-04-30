using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.BusinessLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterAuthenticationBusinessLayer(
            this IServiceCollection services)
        {
            services.AddSingleton<ILoginService, LoginService>();
            return services.AddSingleton<IUserService, UserService>();
        }
    }
}