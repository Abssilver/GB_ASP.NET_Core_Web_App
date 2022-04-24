using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Migrations
{
    static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationDataContext>(options =>
                    {
                        options.UseNpgsql(
                                "Host=localhost;Port=5432;Username=postgres;Password=geekbrainswebapplication;Database=postgres",
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
                    services.AddHostedService<Worker>();
                });
        
        /*public static IConfigurationRoot CreateConfigurationRoot()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }*/
    }
}