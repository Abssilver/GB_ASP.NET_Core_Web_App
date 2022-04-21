using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public sealed class ClientRepository : BaseEntityRepository<Client>, IClientRepository
    {
        protected override DbSet<Client> DbSet { get; set; }

        public ClientRepository(ApplicationDataContext context) : base(context)
        {
            DbSet = context.Clients;
        }
    }
    /*
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDataContext _context;

        public ClientRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients
                .Where(entity => entity.Id == id)
                .FirstAsync();
        }

        public async Task<Client> GetByNameAsync(string name)
        {
            return await _context.Clients
                .Where(entity => entity.Name.Equals(name))
                .FirstAsync();
        }

        public async Task<IEnumerable<Client>> GetAsync(int page, int take)
        {
            return await _context.Clients
                .Skip(page * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task CreateAsync(Client item)
        {
            _context.Clients.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Client item)
        {
            _context.Clients.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Clients
                .Where(e => e.Id == id)
                .FirstAsync();
            _context.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
    */
}