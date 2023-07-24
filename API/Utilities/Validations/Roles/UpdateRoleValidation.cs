﻿using API.Contracts;
using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class UpdateRoleValidation:AbstractValidator<GetViewRoleDto>
    {
        private readonly IRoleRepository _repository;

        public UpdateRoleValidation(IRoleRepository repository)
        {
            _repository = repository;
            RuleFor(room => room.Name)
                .NotEmpty().WithMessage("Name can not be Null")
                .Must(IsDuplicateValue).WithMessage("Name Already Saved");
        }
        private bool IsDuplicateValue(string arg)
        {
            return _repository.IsNotExist(arg);
        }
    }
}
