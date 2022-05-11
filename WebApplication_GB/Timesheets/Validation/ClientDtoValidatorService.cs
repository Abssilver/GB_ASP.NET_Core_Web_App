using DataLayer;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Timesheets.Validation
{
    public interface IClientDtoValidatorService: IValidationService<ClientDto>
    { }

    internal sealed class ClientDtoValidatorService : FluentValidationService<ClientDto>, 
        IClientDtoValidatorService
    {
        public ClientDtoValidatorService()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id не должен быть пустым или отрицательным")
                .WithErrorCode("SKY-DTO-100.1");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name не должно быть пустым")
                .WithErrorCode("SKY-DTO-100.2");
        }
    }
}