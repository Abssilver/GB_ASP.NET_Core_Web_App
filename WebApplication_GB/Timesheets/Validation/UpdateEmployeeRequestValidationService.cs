using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IUpdateEmployeeRequestValidationService: IValidationService<UpdateEmployeeRequest>
    { }

    internal sealed class UpdateEmployeeRequestValidationService : FluentValidationService<UpdateEmployeeRequest>, 
        IUpdateEmployeeRequestValidationService
    {
        public UpdateEmployeeRequestValidationService()
        {
            RuleFor(x => x.Employee)
                .NotNull()
                .WithMessage("Employee не должен быть пустым")
                .WithErrorCode("SKY-2000.1");
            
            RuleFor(x => x.Employee)
                .SetValidator(new EmployeeDtoValidatorService());
        }
    }
}