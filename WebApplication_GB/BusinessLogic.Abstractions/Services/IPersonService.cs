using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.DTO;

namespace BusinessLogic.Abstractions.Services
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonDto>> GetEntitiesAsync(int skip, int take);
    }
}