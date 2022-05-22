using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetClientsWithPaginationRequestValidationService: IValidationService<GetClientsWithPaginationRequest>
    { }

    internal sealed class GetClientsWithPaginationRequestValidationService : FluentValidationService<GetClientsWithPaginationRequest>, 
        IGetClientsWithPaginationRequestValidationService
    {
        public GetClientsWithPaginationRequestValidationService()
        {
            RuleFor(x => x.PageNumber)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("PageNumber не должен быть пустым и отрицательным")
                .WithErrorCode("SKY-800.1");
            
            RuleFor(x => x.ElementsPerPage)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("ElementsPerPage не должен быть пустым и меньше 1")
                .WithErrorCode("SKY-800.2");
        }
    }
}