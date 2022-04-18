using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.DTO;

namespace BusinessLogic.Abstractions.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetPersonsAsync();
    }
}