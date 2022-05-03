namespace Timesheets.Requests
{
    public class GetContractsWithPaginationRequest
    {
        public int PageNumber { get; set; }
        public int ElementsPerPage { get; set; }
    }
}