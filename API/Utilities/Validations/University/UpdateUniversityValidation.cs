using API.Contracts;
using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.University
{
    public class UpdateUniversityValidation:AbstractValidator<NewUniversityDto>
    {
        private readonly IUniversityRepository _repository;

        public UpdateUniversityValidation(IUniversityRepository repository)
        {
            _repository = repository;
            RuleFor(university => university.Name)
                .NotEmpty().WithMessage("Name can not be Null")
                .Must(IsDuplicateValue).WithMessage("Name Already Saved");
            RuleFor(university => university.Code)
                .NotEmpty().WithMessage("Code can not be Null")
                .Must(IsDuplicateValue).WithMessage("Code Already Saved");
        }
        private bool IsDuplicateValue(string arg)
        {
            return _repository.IsNotExist(arg);
        }
    }
}
