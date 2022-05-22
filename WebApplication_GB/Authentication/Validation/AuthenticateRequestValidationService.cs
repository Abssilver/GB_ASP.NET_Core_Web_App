using Authentication.Requests;
using FluentValidation;
using Validation.Abstractions.Services.Abstractions;

namespace Authentication.Validation
{
    public interface IAuthenticateRequestValidationService: IValidationService<AuthenticateRequest>
    { }

    internal sealed class AuthenticateRequestValidationService : FluentValidationService<AuthenticateRequest>, 
        IAuthenticateRequestValidationService
    {
        public AuthenticateRequestValidationService()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Логин не должен быть пустым")
                .WithErrorCode("SKY-200.1");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым")
                .WithErrorCode("SKY-200.2");
        }
    }
}