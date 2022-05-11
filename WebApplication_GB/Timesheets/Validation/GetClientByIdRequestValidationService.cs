using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetClientByIdRequestValidationService: IValidationService<GetClientByIdRequest>
    { }

    internal sealed class GetClientByIdRequestValidationService : FluentValidationService<GetClientByIdRequest>, 
        IGetClientByIdRequestValidationService
    {
        public GetClientByIdRequestValidationService()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("ClientId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-600.1");
        }
    }
}