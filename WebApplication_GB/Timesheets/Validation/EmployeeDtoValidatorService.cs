using DataLayer;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IEmployeeDtoValidatorService: IValidationService<EmployeeDto>
    { }

    internal sealed class EmployeeDtoValidatorService : FluentValidationService<EmployeeDto>, 
        IEmployeeDtoValidatorService
    {
        public EmployeeDtoValidatorService()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-400.1");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name не должно быть пустым")
                .WithErrorCode("SKY-DTO-400.2");
            
            RuleFor(x => x.Salary)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Salary не должно быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-400.3");
            
            RuleFor(x => x.Time)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Time не должно быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-400.4");
        }
    }
}