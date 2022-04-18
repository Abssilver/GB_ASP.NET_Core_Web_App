using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLogic.Abstractions.Services
{
    public interface IContractService
    {
        public Task<ContractDto> GetContractByIdAsync(int id);
        public Task<ContractDto> GetContractByNameAsync(string name);
        public Task<IEnumerable<ContractDto>> GetContractsAsync(int skip, int take);
        public Task CreateContractAsync(ContractDto item);
        public Task UpdateContractAsync(ContractDto item);
        public Task DeleteContractAsync(int id);
    }
}