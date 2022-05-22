using Authentication.Requests;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Authentication.Validation
{
    public interface ICreateUserRequestValidationService: IValidationService<CreateUserRequest>
    { }

    internal sealed class CreateUserRequestValidationService : FluentValidationService<CreateUserRequest>, 
        ICreateUserRequestValidationService
    {
        public CreateUserRequestValidationService()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым")
                .WithErrorCode("SKY-100.1");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("Пароль не должен быть пустым и короче 4 символов")
                .WithErrorCode("SKY-100.2");
            
            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(x => string.CompareOrdinal(x.ToLower(), "user") == 0)
                .WithMessage("Должна быть назначена роль User")
                .WithErrorCode("SKY-100.3");
        }
    }
}