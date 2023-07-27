using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
    {
        public readonly IEmployeeRepository _employeerepository;
        public ForgotPasswordValidator(IEmployeeRepository employeerepository = null)
        {
            _employeerepository = employeerepository;
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Must Be an Email Address");
        }
    }
}
