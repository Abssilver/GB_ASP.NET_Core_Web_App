using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsAsync();
    }
}