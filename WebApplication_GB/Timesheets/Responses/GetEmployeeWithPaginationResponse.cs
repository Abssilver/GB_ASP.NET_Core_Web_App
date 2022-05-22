using System.Collections.Generic;
using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetEmployeeWithPaginationResponse: ApiResponse
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}