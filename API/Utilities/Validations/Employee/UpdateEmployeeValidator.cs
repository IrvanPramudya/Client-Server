using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<GetViewEmployeeDto>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeValidator(IEmployeeRepository repository)
        {
            _repository = repository;
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First Name is Required");
            RuleFor(employee => employee.BirthDate)
                .NotEmpty().WithMessage("Birth Date is Required")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-17)).WithMessage("Must be Above 17 Years Old");
            RuleFor(employee => employee.Gender)
                .IsInEnum().WithMessage("Gender is not Identify");
            RuleFor(employee => employee.HiringDate)
                .NotEmpty().WithMessage("Hiring Date is Required");
            RuleFor(employee => employee.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Wrong Email")
                .Must((e,g)=> IsDuplicateValue(e.Email,e.Guid)).WithMessage("Email is Already Exist");
            RuleFor(employee => employee.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is Required")
                .MaximumLength(20).WithMessage("Maximum 20 Character")
                .Matches(@"^\+[0-9]")
                .Must((e, g) => IsDuplicateValue(e.PhoneNumber, e.Guid)).WithMessage("Phone is Already Exist");

        }

        private bool IsDuplicateValue(string arg,Guid guid)
        {
            var temp = false;
            var (email,phone) = GetEmail(guid);
            if(arg == email||arg== phone) 
            { 
                temp = true; 
            }
            var result = _repository.IsNotExist(arg) || temp;
            return result;
        }
        private (string email, string phone) GetEmail(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            return (data.Email, data.PhoneNumber);
        }
    }
}
