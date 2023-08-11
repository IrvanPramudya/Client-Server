using API.Models;
using Client.Contracts;

namespace Client.Repositories
{
    public class EmployeeRepository : TableRepository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(string request = "employee/") : base(request)
        {
        }
    }
}
