using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public interface IContractRepository
    {
        public Task<Contract> GetByIdAsync(int id);
        public Task<Contract> GetByNameAsync(string name);
        public Task<IEnumerable<Contract>> GetAsync(int skip, int take);
        public Task CreateAsync(Contract item);
        public Task UpdateAsync(Contract item);
        public Task DeleteAsync(int id);
    }
}