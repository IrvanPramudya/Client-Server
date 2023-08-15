using API.Models;
using API.Utilities.Enums;

namespace API.Contracts
{
    public interface IEmployeeRepository : ITableRepository<Employee>
    {
        bool IsNotExist(string value);
        string? GetLastNik();
        Employee? GetEmail(string email);
        Employee CheckEmail(string email);
    }
}
