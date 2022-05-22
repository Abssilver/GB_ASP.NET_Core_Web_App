using Authentication.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Authentication.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(b => b.AddUserSecrets<Program>())
                .ConfigureServices((host, services) =>
                {
                    services
                        .AddDbContext<AuthenticationDataContext>(options =>
                            options.UseNpgsql(host.Configuration.GetConnectionString("DefaultConnection"),
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
                    services.AddHostedService<AuthenticationWorker>();
                });
    }
}