using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IDeleteContractRequestValidationService: IValidationService<DeleteContractRequest>
    { }

    internal sealed class DeleteContractRequestValidationService : FluentValidationService<DeleteContractRequest>, 
        IDeleteContractRequestValidationService
    {
        public DeleteContractRequestValidationService()
        {
            RuleFor(x => x.ContractId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("ContractId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-400.1");
        }
    }
}