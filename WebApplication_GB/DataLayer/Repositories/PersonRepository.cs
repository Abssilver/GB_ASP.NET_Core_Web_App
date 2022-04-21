using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public sealed class PersonRepository : BaseEntityRepository<Person>, IPersonRepository
    {
        protected override DbSet<Person> DbSet { get; set; }
        public PersonRepository(ApplicationDataContext context) : base(context)
        {
            DbSet = context.Persons;
        }
    }

    /*
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDataContext _context;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Where(entity => entity.Id == id)
                .FirstAsync();
        }

        public async Task<Person> GetByNameAsync(string name)
        {
            return await _context.Persons
                .Where(entity => entity.Name.Equals(name))
                .FirstAsync();
        }

        public async Task<IEnumerable<Person>> GetAsync(int page, int take)
        {
            return await _context.Persons
                .Skip(page * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task CreateAsync(Person item)
        {
            _context.Persons.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Person item)
        {
            _context.Persons.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Persons
                .Where(e => e.Id == id)
                .FirstAsync();
            _context.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
    */
}