using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetClientByNameResponse: ApiResponse
    {
        public ClientDto Client { get; set; }
    }
}