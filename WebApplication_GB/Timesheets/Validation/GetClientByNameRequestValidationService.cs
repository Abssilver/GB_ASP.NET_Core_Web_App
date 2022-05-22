using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetClientByNameRequestValidationService: IValidationService<GetClientByNameRequest>
    { }

    internal sealed class GetClientByNameRequestValidationService : FluentValidationService<GetClientByNameRequest>, 
        IGetClientByNameRequestValidationService
    {
        public GetClientByNameRequestValidationService()
        {
            RuleFor(x => x.ClientName)
                .NotEmpty()
                .WithMessage("ClientName не должен быть пустым")
                .WithErrorCode("SKY-700.1");
        }
    }
}