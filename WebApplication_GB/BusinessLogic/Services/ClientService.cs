using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using DataLayer;
using DataLayer.Abstractions.Repositories;

namespace BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClientDto> GetEntityByIdAsync(long id)
        {
            var result = await _repository.GetByIdAsync(id);
            return new ClientDto
            {
                Id = result.Id,
                Name = result.Name,
            };
        }

        public async Task<ClientDto> GetEntityByNameAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name);
            return new ClientDto
            {
                Id = result.Id,
                Name = result.Name,
            };
        }

        public async Task<IEnumerable<ClientDto>> GetEntitiesAsync(int skip, int take)
        {
            var result = await _repository.GetAsync(skip, take);
            return result.Select(entity => new ClientDto
            {
                Id = entity.Id,
                Name = entity.Name,
            }).ToArray();
        }

        public Task CreateAsync(ClientDto item)
        {
            return _repository.CreateAsync(new Client
            {
                Id = item.Id.Value,
                Name = item.Name,
            });
        }

        public async Task UpdateAsync(ClientDto item)
        {
            var entity = await _repository.GetByIdAsync(item.Id.Value);
            entity.Name = item.Name;
            await _repository.UpdateAsync(entity);
        }

        public Task DeleteAsync(long id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}