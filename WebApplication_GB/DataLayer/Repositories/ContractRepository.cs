using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDataContext _context;

        public ContractRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _context.Contracts
                .Where(contact => contact.Id == id)
                .FirstAsync();
        }

        public async Task<Contract> GetByNameAsync(string name)
        {
            return await _context.Contracts
                .Where(contact => contact.Name.Equals(name))
                .FirstAsync();
        }

        public async Task<IEnumerable<Contract>> GetAsync(int page, int take)
        {
            return await _context.Contracts
                .Skip(page * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task CreateAsync(Contract item)
        {
            _context.Contracts.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Contract item)
        {
            _context.Contracts.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await _context.Contracts
                .Where(entity => entity.Id == id)
                .FirstAsync();
            _context.Remove(contract);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}