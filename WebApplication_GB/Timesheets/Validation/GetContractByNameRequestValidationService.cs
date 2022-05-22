using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetContractByNameRequestValidationService: IValidationService<GetContractByNameRequest>
    { }

    internal sealed class GetContractByNameRequestValidationService : FluentValidationService<GetContractByNameRequest>, 
        IGetContractByNameRequestValidationService
    {
        public GetContractByNameRequestValidationService()
        {
            RuleFor(x => x.ContractName)
                .NotEmpty()
                .WithMessage("ContractName не должен быть пустым")
                .WithErrorCode("SKY-1000.1");
        }
    }
}