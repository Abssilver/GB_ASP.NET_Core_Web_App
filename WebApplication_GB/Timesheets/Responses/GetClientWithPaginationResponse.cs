using System.Collections.Generic;
using DataLayer;

namespace Timesheets.Requests
{
    public class GetClientWithPaginationResponse
    {
        public IEnumerable<ClientDto> Clients { get; set; }
    }
}