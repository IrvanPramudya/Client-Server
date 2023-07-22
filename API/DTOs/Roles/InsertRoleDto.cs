using API.Models;

namespace API.DTOs.Roles
{
    public class InsertRoleDto
    {

        public string Name { get; set; }

        public static implicit operator Role(InsertRoleDto insertRoleDto)
        {
            return new Role
            {
                Guid = new Guid(),
                Name = insertRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator InsertRoleDto(Role role)
        {
            return new InsertRoleDto
            {
                Name = role.Name,
            };
        }
    }
}
