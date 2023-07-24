using API.Models;

namespace API.Contracts
{
    public interface IRoleRepository : ITableRepository<Role>
    {
        bool IsNotExist(string value);
    }
}
