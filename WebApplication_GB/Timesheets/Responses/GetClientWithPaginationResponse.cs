using System.Collections.Generic;
using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetClientWithPaginationResponse: ApiResponse
    {
        public IEnumerable<ClientDto> Clients { get; set; }
    }
}