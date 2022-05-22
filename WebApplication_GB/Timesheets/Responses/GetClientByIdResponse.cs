using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetClientByIdResponse: ApiResponse
    {
        public ClientDto Client { get; set; }
    }
}