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

        public async Task<IEnumerable<PersonDto>> GetPersonsAsync()
        {
            var result = await _repository.GetPersonsAsync();
            return result.Select(r => new PersonDto { Id = r.Id, Name = r.Name }).ToArray();
        }
    }
}