using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRoleRepository : TableRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingDbContext context) : base(context)
        {
        }

        public IEnumerable<string> GetRolesNameByAccountGuid(Guid guid)
        {
            var data = _context.Set<AccountRole>()
                        .Where(ar=>ar.AccountGuid == guid)
                        .Include(ar=>ar.Role)
                        .Select(ar=>ar.Role!.Name);
            return data;
        }
    }
}
