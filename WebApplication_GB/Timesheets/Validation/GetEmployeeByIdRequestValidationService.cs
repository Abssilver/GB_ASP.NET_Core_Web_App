using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetEmployeeByIdRequestValidationService: IValidationService<GetEmployeeByIdRequest>
    { }

    internal sealed class GetEmployeeByIdRequestValidationService : FluentValidationService<GetEmployeeByIdRequest>, 
        IGetEmployeeByIdRequestValidationService
    {
        public GetEmployeeByIdRequestValidationService()
        {
            RuleFor(x => x.EmployeeId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("EmployeeId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-1200.1");
        }
    }
}