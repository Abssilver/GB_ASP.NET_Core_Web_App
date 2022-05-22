namespace Timesheets.Requests
{
    public class GetEmployeesWithPaginationRequest
    {
        public int? PageNumber { get; set; }
        public int? ElementsPerPage { get; set; }
    }
}