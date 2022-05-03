using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;

namespace DataLayer.Abstractions.Repositories
{
    public interface IEntitiesRepository<TEntity> where TEntity: BaseEntity
    {
        public Task<TEntity> GetByIdAsync(long id);
        public Task<TEntity> GetByNameAsync(string name);
        public Task<IEnumerable<TEntity>> GetAsync(int skip, int take);
        public Task CreateAsync(TEntity item);
        public Task UpdateAsync(TEntity item);
        public Task DeleteAsync(long id);
    }
}