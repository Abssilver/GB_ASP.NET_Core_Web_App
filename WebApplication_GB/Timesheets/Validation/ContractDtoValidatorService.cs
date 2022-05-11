using DataLayer;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IContractDtoValidatorService: IValidationService<ContractDto>
    { }

    internal sealed class ContractDtoValidatorService : FluentValidationService<ContractDto>, 
        IContractDtoValidatorService
    {
        public ContractDtoValidatorService()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-200.1");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name не должно быть пустым")
                .WithErrorCode("SKY-DTO-200.2");
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description не должно быть пустым")
                .WithErrorCode("SKY-DTO-200.3");
            
            RuleFor(x => x.Tasks)
                .NotNull()
                .Must(tasks => tasks.Count > 0)
                .WithMessage("Tasks не должно быть пустым и количество должно быть больше 0")
                .WithErrorCode("SKY-DTO-200.4");
            
            RuleFor(x => x.Owner)
                .NotNull()
                .WithMessage("Owner не должен быть пустым")
                .WithErrorCode("SKY-DTO-200.5");

            RuleForEach(x => x.Tasks)
                .SetValidator(new ContractTaskDtoValidatorService());

            RuleFor(x => x.Owner)
                .SetValidator(new ClientDtoValidatorService());
        }
    }
}