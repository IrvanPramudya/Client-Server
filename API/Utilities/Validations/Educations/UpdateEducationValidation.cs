using API.Contracts;
using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class UpdateEducationValidation: AbstractValidator<GetViewEducationDto>
    {

        public UpdateEducationValidation()
        {
            RuleFor(education => education.Guid)
                .NotEmpty().WithMessage("Guid Can Not Null");
            RuleFor(education => education.Major)
                .NotEmpty().WithMessage("Major Can Not Null");
            RuleFor(education => education.Degree)
                .NotEmpty().WithMessage("Degree Can Not Null");
            RuleFor(education => education.Gpa)
                .NotEmpty().WithMessage("Gpa Can Not Null");
            RuleFor(education => education.UniversityGuid)
                .NotEmpty().WithMessage("University Guid Can Not Null");
        }
    }
}
