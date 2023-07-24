using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : ITableRepository<Employee>
    {
        bool IsNotExist(string value);
        string? GetLastNik();
    }
}
