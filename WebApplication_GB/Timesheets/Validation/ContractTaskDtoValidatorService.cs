using DataLayer;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IContractTaskDtoValidatorService: IValidationService<ContractTaskDto>
    { }

    internal sealed class ContractTaskDtoValidatorService : FluentValidationService<ContractTaskDto>, 
        IContractTaskDtoValidatorService
    {
        public ContractTaskDtoValidatorService()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-300.1");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name не должно быть пустым")
                .WithErrorCode("SKY-DTO-300.2");
            
            RuleFor(x => x.Time)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Time не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-300.3");
        }
    }
}