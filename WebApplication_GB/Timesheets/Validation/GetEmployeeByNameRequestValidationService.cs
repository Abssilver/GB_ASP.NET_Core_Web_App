using FluentValidation;
using Timesheets.Requests;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IGetEmployeeByNameRequestValidationService: IValidationService<GetEmployeeByNameRequest>
    { }

    internal sealed class GetEmployeeByNameRequestValidationService : FluentValidationService<GetEmployeeByNameRequest>, 
        IGetEmployeeByNameRequestValidationService
    {
        public GetEmployeeByNameRequestValidationService()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty()
                .WithMessage("EmployeeName не должен быть пустым")
                .WithErrorCode("SKY-1300.1");
        }
    }
}