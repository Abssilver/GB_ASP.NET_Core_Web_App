using System.Threading;
using System.Threading.Tasks;
using Authentication.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Authentication.Migrations
{
    public class AuthenticationWorker : IHostedService
    {
        private readonly AuthenticationDataContext _context;

        public AuthenticationWorker(AuthenticationDataContext context)
        {
            _context = context;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}