using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.BusinessLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterAuthenticationBusinessLayer(
            this IServiceCollection services)
        {
            services.AddTransient<ILoginService, LoginService>();
            return services.AddTransient<IUserService, UserService>();
        }
    }
}