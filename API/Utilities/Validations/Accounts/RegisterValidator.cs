
using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;

        public RegisterValidator(IEmployeeRepository employeeRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
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
                .Must(IsDuplicateValue).WithMessage("Email is Already Exist");
            RuleFor(employee => employee.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is Required")
                .MaximumLength(20).WithMessage("Maximum 20 Character")
                .Matches(@"^\+[0-9]")
                .Must(IsDuplicateValue).WithMessage("Phone is Already Exist");
            RuleFor(education => education.Major)
                .NotEmpty().WithMessage("Major Can Not Null");
            RuleFor(education => education.Degree)
                .NotEmpty().WithMessage("Degree Can Not Null");
            RuleFor(education => education.GPA)
                .NotEmpty().WithMessage("Gpa Can Not Null");
            RuleFor(university => university.UniversityName)
                .NotEmpty().WithMessage("Name can not be Null")
                .Must(IsDuplicateValueUniversity).WithMessage("Name Already Saved");
            RuleFor(university => university.UniversityCode)
                .NotEmpty().WithMessage("Code can not be Null")
                .Must(IsDuplicateValueUniversity).WithMessage("Code Already Saved");
            RuleFor(account => account.Password)
                .NotEmpty().WithMessage("Password can not be Null")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(account => account.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password can not be Null")
                .Equal(account => account.Password).WithMessage("Password Doesnt Match");
            
        }

        private bool IsDuplicateValueUniversity(string arg)
        {
            return _universityRepository.IsNotExist(arg);
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
