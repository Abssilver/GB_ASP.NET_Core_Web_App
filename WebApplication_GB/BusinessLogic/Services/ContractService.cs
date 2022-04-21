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

        public async Task<ContractDto> GetEntityByIdAsync(long id)
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

        public async Task<ContractDto> GetEntityByNameAsync(string name)
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

        public async Task<IEnumerable<ContractDto>> GetEntitiesAsync(int skip, int take)
        {
            var result = await _repository.GetAsync(skip, take);
            return result.Select(entity => new ContractDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tasks = entity.Tasks.Select(task => new ContractTaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Time = task.Time,
                }).ToList(),
                Owner = new ClientDto
                {
                    Id = entity.Owner.Id,
                }
            }).ToArray();
        }

        public Task CreateAsync(ContractDto item)
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

        public async Task UpdateAsync(ContractDto item)
        {
            var entity = await _repository.GetByIdAsync(item.Id);
            entity.Name = item.Name;
            entity.Description = item.Description;
            entity.Tasks = item.Tasks.Select(task => new ContractTask
            {
                Id = task.Id,
                Name = task.Name,
                Time = task.Time,
            }).ToList();
            entity.Owner = new Client { Id = item.Owner.Id, };
            await _repository.UpdateAsync(entity);
        }

        public Task DeleteAsync(long id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}