using Authentication.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Authentication.Migrations
{
    public static class Registration
    {
        public static IServiceCollection RegisterAuthenticationMigrations
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                        builder => builder.MigrationsAssembly("Authentication.Migrations"))
                    .UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddConsole()
                            .AddFilter((category, logLevel) =>
                                category == DbLoggerCategory.Database.Command.Name &&
                                logLevel == LogLevel.Information);
                    }))
            );
            //services.AddHostedService<AuthenticationWorker>();
            return services;
        }
    }
}