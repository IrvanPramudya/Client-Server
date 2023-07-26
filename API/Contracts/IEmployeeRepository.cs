using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : ITableRepository<Employee>
    {
        bool IsNotExist(string value);
        bool IsNotExist2(Guid Guid, string value);
        Guid GetGuid();
        string? GetLastNik();
        Employee? GetEmail(string email);
    }
}
