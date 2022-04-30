using Authentication.BusinessLayer.Abstractions.JwtOptions;
using Authentication.BusinessLayer.Abstractions.Services;
using Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication
{

    public static class Registration
    {
        public static IServiceCollection RegisterAuthentication(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddCors();
            services.Configure<JwtAccessOptions>(configuration.GetSection("Authentication:JwtAccessOptions"));
            services.Configure<JwtRefreshOptions>(configuration.GetSection("Authentication:JwtRefreshOptions"));
            
            var jwtAccessSettings = new JwtOptions();
            var jwtRefreshSettings = new JwtOptions();
            configuration.Bind("Authentication:JwtAccessOptions", jwtAccessSettings);
            configuration.Bind("Authentication:JwtRefreshOptions", jwtRefreshSettings);

             services
                .AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = jwtAccessSettings.GetTokenValidationParameters();
                });
             
             return services;
        }
    }
}