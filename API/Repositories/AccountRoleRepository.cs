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
    }
}
