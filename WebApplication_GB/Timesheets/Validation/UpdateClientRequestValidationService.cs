using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IUpdateClientRequestValidationService: IValidationService<UpdateClientRequest>
    { }

    internal sealed class UpdateClientRequestValidationService : FluentValidationService<UpdateClientRequest>, 
        IUpdateClientRequestValidationService
    {
        public UpdateClientRequestValidationService()
        {
            RuleFor(x => x.Client)
                .NotNull()
                .WithMessage("Client не должен быть пустым")
                .WithErrorCode("SKY-1800.1");
            
            RuleFor(x => x.Client)
                .SetValidator(new ClientDtoValidatorService());
        }
    }
}