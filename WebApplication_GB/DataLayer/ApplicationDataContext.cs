using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationDataContext: DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            
        }

        public DbSet<ContractDto> Contracts { get; set; }
    }
}