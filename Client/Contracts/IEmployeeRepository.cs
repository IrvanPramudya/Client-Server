using API.Models;

namespace Client.Contracts
{
    public interface IEmployeeRepository : ITableRepository<Employee, Guid>
    {
    }
}
