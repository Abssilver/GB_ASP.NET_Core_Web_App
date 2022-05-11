using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetEmployeesWithPaginationRequestValidationService: IValidationService<GetEmployeesWithPaginationRequest>
    { }

    internal sealed class GetEmployeesWithPaginationRequestValidationService : FluentValidationService<GetEmployeesWithPaginationRequest>, 
        IGetEmployeesWithPaginationRequestValidationService
    {
        public GetEmployeesWithPaginationRequestValidationService()
        {
            RuleFor(x => x.PageNumber)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("PageNumber не должен быть пустым и отрицательным")
                .WithErrorCode("SKY-1400.1");
            
            RuleFor(x => x.ElementsPerPage)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("ElementsPerPage не должен быть пустым и меньше 1")
                .WithErrorCode("SKY-1400.2");
        }
    }
}