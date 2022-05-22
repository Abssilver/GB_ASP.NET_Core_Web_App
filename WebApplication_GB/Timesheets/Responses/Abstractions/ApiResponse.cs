using System.Collections.Generic;
using Validation.Abstractions.Entities;

namespace Timesheets.Requests.Abstractions
{
    public abstract class ApiResponse
    {
        public IReadOnlyList<IOperationFailure> Failures { get; set; }
        public bool IsSucceed { get; set; }
    }
}