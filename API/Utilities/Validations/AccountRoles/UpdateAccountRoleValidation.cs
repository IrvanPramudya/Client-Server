using API.Contracts;
using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRoleValidation:AbstractValidator<GetViewAccountRoleDto>
    {
        private readonly IAccountRoleRepository _repository;

        public UpdateAccountRoleValidation(IAccountRoleRepository repository)
        {
            _repository = repository;
            RuleFor(accounrole => accounrole.AccountGuid)
                .NotEmpty().WithMessage("Account Guid can not be Null");
            RuleFor(accounrole => accounrole.RoleGuid)
                .NotEmpty().WithMessage("Role Guid can not be Null");
        }
    }
}
