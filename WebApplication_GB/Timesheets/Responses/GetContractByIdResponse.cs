using DataLayer;
using Timesheets.Requests.Abstractions;

namespace Timesheets.Requests
{
    public class GetContractByIdResponse: ApiResponse
    {
        public ContractDto Contract { get; set; }
    }
}