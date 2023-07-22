using API.Models;

namespace API.DTOs.Roles
{
    public class GetViewRoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(GetViewRoleDto getViewRoleDto)
        {
            return new Role
            {
                Guid = getViewRoleDto.Guid,
                Name = getViewRoleDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator GetViewRoleDto(Role role)
        {
            return new GetViewRoleDto
            {
                Guid = role.Guid,
                Name = role.Name,
            };
        }
    }
}
