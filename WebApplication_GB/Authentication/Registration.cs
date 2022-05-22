using System.Security.Claims;
using Authentication.BusinessLayer.Abstractions.JwtOptions;
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
            services.AddControllers();

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
             
             services.AddAuthorization(options =>
                 options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "User")));
             
             services.AddCors(x => x.AddPolicy("AuthPolicy", b => b
                 .SetIsOriginAllowed(origin => true)
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials()));
             
             return services;
        }
    }
}