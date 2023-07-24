using API.Models;

namespace API.DTOs.AccountRoles
{
    public class GetViewAccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
        
        public static implicit operator AccountRole(GetViewAccountRoleDto dto)
        {
            return new AccountRole
            {
                Guid            = dto.Guid,
                AccountGuid     = dto.AccountGuid,
                RoleGuid        = dto.RoleGuid,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator GetViewAccountRoleDto(AccountRole accountRole)
        {
            return new GetViewAccountRoleDto
            {
                Guid            = accountRole.Guid,
                AccountGuid     = accountRole.AccountGuid,
                RoleGuid        = accountRole.RoleGuid,
            };
        }
    }
}
