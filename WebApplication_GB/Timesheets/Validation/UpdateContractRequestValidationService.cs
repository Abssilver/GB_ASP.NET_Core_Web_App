using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IUpdateContractRequestValidationService: IValidationService<UpdateContractRequest>
    { }

    internal sealed class UpdateContractRequestValidationService : FluentValidationService<UpdateContractRequest>, 
        IUpdateContractRequestValidationService
    {
        public UpdateContractRequestValidationService()
        {
            RuleFor(x => x.Contract)
                .NotNull()
                .WithMessage("Contract не должен быть пустым")
                .WithErrorCode("SKY-1900.1");
            
            RuleFor(x => x.Contract)
                .SetValidator(new ContractDtoValidatorService());
        }
    }
}