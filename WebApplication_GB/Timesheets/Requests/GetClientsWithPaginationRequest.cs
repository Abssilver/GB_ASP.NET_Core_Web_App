﻿namespace Timesheets.Requests
{
    public class GetClientsWithPaginationRequest
    {
        public int PageNumber { get; set; }
        public int ElementsPerPage { get; set; }
    }
}