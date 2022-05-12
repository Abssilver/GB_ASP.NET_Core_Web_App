using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetEmployeeByIdResponse: ApiResponse
    {
        public EmployeeDto Employee { get; set; }
    }
}