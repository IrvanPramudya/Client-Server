using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class UpdateAccountValidation:AbstractValidator<GetViewAccountDto>
    {
        public UpdateAccountValidation()
        {
            RuleFor(account => account.Password)
                .NotEmpty().WithMessage("Password can not be Null")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(account => account.Otp)
                .NotEmpty().WithMessage("OTP can not be Null");
            RuleFor(account =>account.ExpiredTime)
                .NotEmpty().WithMessage("Fill Expired Time Please")
                .GreaterThan(DateTime.Now.AddDays(30));
        }
    }
}
