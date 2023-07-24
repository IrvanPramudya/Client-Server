using API.Models;

namespace API.DTOs.AccountRoles
{
    public class InsertAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(InsertAccountRoleDto dto)
        {
            return new AccountRole
            {
                Guid            = new Guid(),
                AccountGuid     = dto.AccountGuid,
                RoleGuid        = dto.RoleGuid,
                CreatedDate     = DateTime.Now,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator InsertAccountRoleDto(AccountRole accountRole)
        {
            return new InsertAccountRoleDto
            {
                AccountGuid     = accountRole.AccountGuid,
                RoleGuid        = accountRole.RoleGuid
            };
        }
    }
}
