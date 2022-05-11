using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IRegisterContractRequestValidationService: IValidationService<RegisterContractRequest>
    { }

    internal sealed class RegisterContractRequestValidationService : FluentValidationService<RegisterContractRequest>, 
        IRegisterContractRequestValidationService
    {
        public RegisterContractRequestValidationService()
        {
            RuleFor(x => x.Contract)
                .NotNull()
                .WithMessage("Contract не должен быть пустым")
                .WithErrorCode("SKY-1600.1");
            
            RuleFor(x => x.Contract)
                .SetValidator(new ContractDtoValidatorService());
        }
    }
}