using System.Collections.Generic;
using Timesheets.Requests.Abstractions;
using Validation.Abstractions.Entities;

namespace Timesheets.Requests.Extensions
{
    public static class ApiResponseExtension
    {
        public static TResponse Success<TResponse>(this TResponse response) 
            where TResponse: ApiResponse
        {
            response.Failures = new List<IOperationFailure>();
            response.IsSucceed = true;
            return response;
        }
        
        public static TResponse Failure<TResponse>(this TResponse response, IReadOnlyList<IOperationFailure> failures) 
            where TResponse: ApiResponse
        {
            response.Failures = failures;
            response.IsSucceed = false;
            return response;
        }
    }
}