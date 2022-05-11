using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IDeleteEmployeeRequestValidationService: IValidationService<DeleteEmployeeRequest>
    { }

    internal sealed class DeleteEmployeeRequestValidationService : FluentValidationService<DeleteEmployeeRequest>, 
        IDeleteEmployeeRequestValidationService
    {
        public DeleteEmployeeRequestValidationService()
        {
            RuleFor(x => x.EmployeeId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("EmployeeId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-500.1");
        }
    }
}