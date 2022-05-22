using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetContractByNameResponse: ApiResponse
    {
        public ContractDto Contract { get; set; }
    }
}