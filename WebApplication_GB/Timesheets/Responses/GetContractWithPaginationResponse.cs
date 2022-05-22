using System.Collections.Generic;
using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetContractWithPaginationResponse: ApiResponse
    {
        public IEnumerable<ContractDto> Contracts { get; set; }
    }
}