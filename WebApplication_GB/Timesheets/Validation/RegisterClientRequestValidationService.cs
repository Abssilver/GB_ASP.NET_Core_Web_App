using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IRegisterClientRequestValidationService: IValidationService<RegisterClientRequest>
    { }

    internal sealed class RegisterClientRequestValidationService : FluentValidationService<RegisterClientRequest>, 
        IRegisterClientRequestValidationService
    {
        public RegisterClientRequestValidationService()
        {
            RuleFor(x => x.Client)
                .NotNull()
                .WithMessage("Client не должен быть пустым")
                .WithErrorCode("SKY-1500.1");
            
            RuleFor(x => x.Client)
                .SetValidator(new ClientDtoValidatorService());
        }
    }
}