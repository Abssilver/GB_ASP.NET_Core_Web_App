using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using DataLayer;
using DataLayer.Abstractions.Repositories;

namespace BusinessLogic.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _repository;

        public ContractService(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContractDto> GetContractByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return await Task.FromResult(new ContractDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Tasks = result.Tasks.Select(task => new ContractTaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Time = task.Time,
                }).ToList(),
                Owner = new ClientDto
                {
                    Id = result.Owner.Id,
                }
            });
        }

        public async Task<ContractDto> GetContractByNameAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name);
            return await Task.FromResult(new ContractDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Tasks = result.Tasks.Select(task => new ContractTaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Time = task.Time,
                }).ToList(),
                Owner = new ClientDto
                {
                    Id = result.Owner.Id,
                }
            });
        }

        public async Task<IEnumerable<ContractDto>> GetContractsAsync(int skip, int take)
        {
            var result = await _repository.GetAsync(skip, take);
            return result.Select(contract => new ContractDto
            {
                Id = contract.Id,
                Name = contract.Name,
                Description = contract.Description,
                Tasks = contract.Tasks.Select(task => new ContractTaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Time = task.Time,
                }).ToList(),
                Owner = new ClientDto
                {
                    Id = contract.Owner.Id,
                }
            }).ToArray();
        }

        public Task CreateContractAsync(ContractDto item)
        {
            return _repository.CreateAsync(new Contract
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Tasks = item.Tasks.Select(task => new ContractTask()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Time = task.Time,
                }).ToList(),
                Owner = new Client
                {
                    Id = item.Owner.Id,
                }
            });
        }

        public async Task UpdateContractAsync(ContractDto item)
        {
            var contract = await _repository.GetByIdAsync(item.Id);
            contract.Name = item.Name;
            contract.Description = item.Description;
            contract.Tasks = item.Tasks.Select(task => new ContractTask
            {
                Id = task.Id,
                Name = task.Name,
                Time = task.Time,
            }).ToList();
            contract.Owner = new Client { Id = item.Owner.Id, };
            await _repository.UpdateAsync(contract);
        }

        public Task DeleteContractAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}