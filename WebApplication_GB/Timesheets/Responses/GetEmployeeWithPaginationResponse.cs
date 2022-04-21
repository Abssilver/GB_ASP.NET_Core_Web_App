using System.Collections.Generic;
using DataLayer;

namespace Timesheets.Requests
{
    public class GetEmployeeWithPaginationResponse
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}