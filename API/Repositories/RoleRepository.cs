using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : TableRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookingDbContext context) : base(context)
        {
        }

        public bool IsNotExist(string value)
        {
            return _context.Set<Role>().SingleOrDefault(role => role.Name.Contains(value))is null;
        }
    }
}
