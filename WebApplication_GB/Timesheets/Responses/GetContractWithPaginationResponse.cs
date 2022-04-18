using System.Collections.Generic;
using DataLayer;

namespace Timesheets.Requests
{
    public class GetContractWithPaginationResponse
    {
        public IEnumerable<ContractDto> Contracts { get; set; }
    }
}