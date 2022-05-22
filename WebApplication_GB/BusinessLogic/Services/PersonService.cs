using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.DTO;
using BusinessLogic.Abstractions.Services;
using DataLayer.Abstractions.Repositories;

namespace BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonDto>> GetEntitiesAsync(int skip, int take)
        {
            var result = await _repository.GetAsync(skip, take);
            return result.Select(entity => new PersonDto
            {
                Id = entity.Id,
                Name = entity.Name,
            }).ToArray();
        }
    }
}