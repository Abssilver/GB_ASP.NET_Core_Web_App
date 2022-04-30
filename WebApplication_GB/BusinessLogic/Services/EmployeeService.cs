using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.Services;
using DataLayer;
using DataLayer.Abstractions.Repositories;

namespace BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeDto> GetEntityByIdAsync(long id)
        {
            var result = await _repository.GetByIdAsync(id);
            return new EmployeeDto
            {
                Id = result.Id,
                Name = result.Name,
                Salary = result.Salary,
                Time = result.Time,
            };
        }

        public async Task<EmployeeDto> GetEntityByNameAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name);
            return new EmployeeDto
            {
                Id = result.Id,
                Name = result.Name,
                Salary = result.Salary,
                Time = result.Time,
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetEntitiesAsync(int skip, int take)
        {
            var result = await _repository.GetAsync(skip, take);
            return result.Select(entity => new EmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Salary = entity.Salary,
                Time = entity.Time,
            }).ToArray();
        }

        public Task CreateAsync(EmployeeDto item)
        {
            return _repository.CreateAsync(new Employee
            {
                Id = item.Id,
                Name = item.Name,
                Salary = item.Salary,
                Time = item.Time,
            });
        }

        public async Task UpdateAsync(EmployeeDto item)
        {
            var entity = await _repository.GetByIdAsync(item.Id);
            entity.Name = item.Name;
            entity.Salary = item.Salary;
            entity.Time = item.Time;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}