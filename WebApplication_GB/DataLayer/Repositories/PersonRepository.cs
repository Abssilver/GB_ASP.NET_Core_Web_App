using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _context.Persons.ToArrayAsync();
        }
        
        public async Task<IEnumerable<Person>> GetPersonsAsync(string search, int page, int size)
        {
            return await _context.Persons
                .Where(p => p.Name.Contains($"%{search}%") && p.Id > 10)
                .Skip(page * size)
                .Take(size)
                .ToArrayAsync();
        }
    }
}