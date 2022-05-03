using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.DTO;

namespace BusinessLogic.Abstractions.Services
{
    public interface IEntitiesService<TEntity> where TEntity: BaseDtoEntity
    {
        public Task<TEntity> GetEntityByIdAsync(long id);
        public Task<TEntity> GetEntityByNameAsync(string name);
        public Task<IEnumerable<TEntity>> GetEntitiesAsync(int skip, int take);
        public Task CreateAsync(TEntity item);
        public Task UpdateAsync(TEntity item);
        public Task DeleteAsync(long id);
    }
}