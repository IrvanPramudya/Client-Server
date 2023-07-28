using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class InsertAccountValidation:AbstractValidator<InsertAccountDto>
    {
        public InsertAccountValidation()
        {
            RuleFor(account => account.Password)
                .NotEmpty().WithMessage("Password can not be Null")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("Password Must Contain 1 Lower Case, 1 Upper Case, 1 Number, 1 Symbol, and Minimal 8 Characters");
            RuleFor(account => account.Otp)
                .NotEmpty().WithMessage("OTP can not be Null");
            RuleFor(account =>account.ExpiredTime)
                .NotEmpty().WithMessage("Fill Expired Time Please")
                .GreaterThan(DateTime.Now.AddDays(30));
        }
    }
}
