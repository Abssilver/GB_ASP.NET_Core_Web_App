using DataLayer.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public sealed class EmployeeRepository : BaseEntityRepository<Employee>, IEmployeeRepository
    {
        protected override DbSet<Employee> DbSet { get; set; }

        public EmployeeRepository(ApplicationDataContext context) : base(context)
        {
            DbSet = context.Employees;
        }
    }
    /*
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDataContext _context;

        public EmployeeRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Where(entity => entity.Id == id)
                .FirstAsync();
        }

        public async Task<Employee> GetByNameAsync(string name)
        {
            return await _context.Employees
                .Where(entity => entity.Name.Equals(name))
                .FirstAsync();
        }

        public async Task<IEnumerable<Employee>> GetAsync(int page, int take)
        {
            return await _context.Employees
                .Skip(page * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task CreateAsync(Employee item)
        {
            _context.Employees.Add(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(Employee item)
        {
            _context.Employees.Update(item);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Employees
                .Where(e => e.Id == id)
                .FirstAsync();
            _context.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
    */
}