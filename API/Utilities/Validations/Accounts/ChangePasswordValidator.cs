using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(employee=>employee.Email)
                .NotEmpty().WithMessage("Email Can not be Null")
                .EmailAddress().WithMessage("Email must be on Email Format");
            RuleFor(account => account.Otp)
                .NotEmpty().WithMessage("OTP Can not be Null");
            RuleFor(account => account.NewPassword)
                .NotEmpty().WithMessage("Password Can not be Null")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(account => account.ConfirmPasssword)
                .NotEmpty().WithMessage("Confirm Password Can not be Null")
                .Equal(account=>account.NewPassword).WithMessage("Password Correct")
                .WithMessage("Password Doesnt Match");
        }
    }
}
