using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public abstract class BaseEntityRepository<TEntity> : IEntitiesRepository<TEntity> 
        where TEntity : BaseEntity
    {
        private readonly ApplicationDataContext _context;
        protected abstract DbSet<TEntity> DbSet { set; get; }

        protected BaseEntityRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet
                .Where(entity => entity.Id == id)
                .FirstAsync();
        }

        public async Task<TEntity> GetByNameAsync(string name)
        {
            return await DbSet
                .Where(entity => entity.Name.Equals(name))
                .FirstAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(int page, int take)
        {
            return await DbSet
                .Skip(page * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task CreateAsync(TEntity item)
        {
            DbSet.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(TEntity item)
        {
            DbSet.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await DbSet
                .Where(e => e.Id == id)
                .FirstAsync();
            _context.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}