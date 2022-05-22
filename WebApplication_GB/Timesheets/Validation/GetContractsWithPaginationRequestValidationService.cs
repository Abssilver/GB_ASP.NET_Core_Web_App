using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetContractsWithPaginationRequestValidationService: IValidationService<GetContractsWithPaginationRequest>
    { }

    internal sealed class GetContractsWithPaginationRequestValidationService : FluentValidationService<GetContractsWithPaginationRequest>, 
        IGetContractsWithPaginationRequestValidationService
    {
        public GetContractsWithPaginationRequestValidationService()
        {
            RuleFor(x => x.PageNumber)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("PageNumber не должен быть пустым и отрицательным")
                .WithErrorCode("SKY-1100.1");
            
            RuleFor(x => x.ElementsPerPage)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("ElementsPerPage не должен быть пустым и меньше 1")
                .WithErrorCode("SKY-1100.2");
        }
    }
}