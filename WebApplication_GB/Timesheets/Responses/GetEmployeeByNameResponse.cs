using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetEmployeeByNameResponse: ApiResponse
    {
        public EmployeeDto Employee { get; set; }
    }
}