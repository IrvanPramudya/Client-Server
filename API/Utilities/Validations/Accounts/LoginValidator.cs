using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class LoginValidator : AbstractValidator<LoginAccountDto>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(login => login.Password)
                .NotEmpty();
        }
    }
}
