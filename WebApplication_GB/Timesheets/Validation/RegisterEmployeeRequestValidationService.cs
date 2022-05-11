using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IRegisterEmployeeRequestValidationService: IValidationService<RegisterEmployeeRequest>
    { }

    internal sealed class RegisterEmployeeRequestValidationService : FluentValidationService<RegisterEmployeeRequest>, 
        IRegisterEmployeeRequestValidationService
    {
        public RegisterEmployeeRequestValidationService()
        {
            RuleFor(x => x.Employee)
                .NotNull()
                .WithMessage("Employee не должен быть пустым")
                .WithErrorCode("SKY-1700.1");
            
            RuleFor(x => x.Employee)
                .SetValidator(new EmployeeDtoValidatorService());
        }
    }
}