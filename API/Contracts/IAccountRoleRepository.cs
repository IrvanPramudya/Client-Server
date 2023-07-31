using API.Models;

namespace API.Contracts
{
    public interface IAccountRoleRepository : ITableRepository<AccountRole>
    {
        IEnumerable<string> GetRolesNameByAccountGuid(Guid guid);
    }
}
