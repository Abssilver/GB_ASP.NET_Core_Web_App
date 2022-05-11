using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetContractByIdRequestValidationService: IValidationService<GetContractByIdRequest>
    { }

    internal sealed class GetContractByIdRequestValidationService : FluentValidationService<GetContractByIdRequest>, 
        IGetContractByIdRequestValidationService
    {
        public GetContractByIdRequestValidationService()
        {
            RuleFor(x => x.ContractId)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("ContractId не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-900.1");
        }
    }
}