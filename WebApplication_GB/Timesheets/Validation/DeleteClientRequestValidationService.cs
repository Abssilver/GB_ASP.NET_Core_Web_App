using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IDeleteClientRequestValidationService: IValidationService<DeleteClientRequest>
    { }

    internal sealed class DeleteClientRequestValidationService : FluentValidationService<DeleteClientRequest>, 
        IDeleteClientRequestValidationService
    {
        public DeleteClientRequestValidationService()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("ClientId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-300.1");
        }
    }
}