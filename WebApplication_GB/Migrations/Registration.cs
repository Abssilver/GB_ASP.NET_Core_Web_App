using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Migrations
{
    public static class Registration
    {
        public static IServiceCollection RegisterMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDataContext>(options =>
            {
                options.UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        builder => builder.MigrationsAssembly(nameof(Migrations)))
                    .UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddConsole(_ => { })
                            .AddFilter((category, level) =>
                                category == DbLoggerCategory.Database.Command.Name
                                && level == LogLevel.Information);
                    }));
            });
            
            //services.AddHostedService<Worker>();
            
            return services;
        }
    }
}